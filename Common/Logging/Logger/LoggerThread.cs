/*
 * This file is part of the AzerothCore Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU Affero General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace AzerothSharp.Logging;

/// <summary>
/// ログ出力用スレッド
/// </summary>
internal sealed class LoggerThread : IDisposable, IHotConfigurable
{
    /// <summary>
    /// The work thread.
    /// </summary>
    private Thread m_workThread;

    /// <summary>
    /// The has data event.
    /// </summary>
    private EventWaitHandle m_hasDataEvent;

    /// <summary>
    /// The m stop event.
    /// </summary>
    private EventWaitHandle m_stopEvent;

    /// <summary>
    /// 受信要求キュー（非同期）
    /// </summary>
    private Queue<LogItem> m_logItemQueue;

    /// <summary>
    /// The appenders.
    /// </summary>
    private List<AppenderBase> m_appenders;

    /// <summary>
    /// ロック用オブジェクト
    /// </summary>
    private static readonly object s_lockObj = new object();

    /// <summary>
    /// ロック用オブジェクト
    /// </summary>
    private static readonly object s_hotConfLockObj = new object();

    /// <summary>
    /// このクラスの唯一インスタンス
    /// </summary>
    private static volatile LoggerThread? s_instance;

    /// <summary>
    /// コンストラクター
    /// </summary>
    LoggerThread()
    {
        m_workThread = new Thread(ConsumeLogItem);
        m_workThread.IsBackground = true;

        m_hasDataEvent = new AutoResetEvent(false);
        m_stopEvent = new AutoResetEvent(false);

        m_logItemQueue = new Queue<LogItem>();

        m_appenders = CreateAppenders(GetAppenderInfoList());

        Start();
    }

    /// <summary>
    /// このクラスの唯一インスタンスを取得
    /// </summary>
    /// <returns>このクラスの唯一インスタンス</returns>
    internal static LoggerThread GetInstance()
    {
        if (s_instance == null)
        {
            lock (s_lockObj)
            {
                if (s_instance == null)
                {
                    s_instance = new LoggerThread();
                }
            }
        }

        return s_instance;
    }

    /// <summary>
    /// Enqueues the log item.
    /// </summary>
    /// <param name="logitem">Logitem.</param>
    internal void EnqueueLogItem(LogItem logitem)
    {
        lock (s_lockObj)
        {
            m_logItemQueue.Enqueue(logitem);
            m_hasDataEvent.Set();
        }
    }

    /// <summary>
    /// 設定ファイルで設定している Appender のリストから参照しないAppenderを除く
    /// </summary>
    /// <returns>appender list</returns>
    internal List<AppenderInfo> GetAppenderInfoList()
    {
        List<AppenderInfo>? appenderList = LoggerSettings.GetInstance().Configuration?.Appenders?.Where(
            (appender) =>
            {
                AppenderRefInfo? appenderRefInfo = LoggerSettings.GetInstance().Configuration?.Logger?.AppenderRefs?.FirstOrDefault(
                    (appenderRef) =>
                    {
                        return appenderRef.AppenderName == appender.AppenderName;
                    }
                );

                return appenderRefInfo != null;
            }
        ).ToList();

        return appenderList ?? new List<AppenderInfo>();
    }

    /// <summary>
    /// Consumes the log item.
    /// </summary>
    internal void ConsumeLogItem()
    {
        try
        {
            LogItem logItem;

            while (true)
            {
                if (m_hasDataEvent.WaitOne(-1))
                {
                    while (true)
                    {
                        lock (s_lockObj)
                        {
                            if (m_logItemQueue.Count > 0)
                            {
                                try
                                {
                                    // 出力データ取得
                                    logItem = m_logItemQueue.Dequeue();
                                }
                                catch (InvalidOperationException)
                                {
                                    // データ無し
                                    break;
                                }
                            }
                            else
                            {
                                // データ無し
                                break;
                            }
                        }

                        lock (s_hotConfLockObj)
                        {
                            // ログ出力
                            foreach (var appender in m_appenders)
                            {
                                try
                                {
                                    appender.Append(logItem);
                                }
                                catch (ThreadAbortException)
                                {
                                    throw;
                                }
                                catch (Exception e)
                                {
                                    Trace.Write(e.StackTrace);
                                    continue;
                                }
                            }
                        }
                    }
                }

                if (m_stopEvent.WaitOne(0))
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Trace.Write(e.StackTrace);
        }
    }

    /// <summary>
    /// Start this thread.
    /// </summary>
    internal void Start()
    {
        if (m_workThread != null)
        {
            m_workThread.Start();
        }
    }

    /// <summary>
    /// Stop this instance.
    /// </summary>
    internal void Stop()
    {
        if (m_workThread != null)
        {

            m_stopEvent.Set();
            m_hasDataEvent.Set();

            m_workThread.Join(3000);
        }
    }

    /// <summary>
    /// Release this instance.
    /// </summary>
    internal void Release()
    {

        m_hasDataEvent.Close();
        m_stopEvent.Close();

        lock (s_lockObj)
        {
            m_logItemQueue.Clear();
        }

        ReleaseAppenders();
    }

    /// <summary>
    /// Creates the appenders.
    /// </summary>
    /// <param name="appendersInfo">Appenders info.</param>
    internal List<AppenderBase> CreateAppenders(List<AppenderInfo> appendersInfo)
    {
        List<AppenderBase> appenders = new List<AppenderBase>();

        foreach (AppenderInfo info in appendersInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(info.AppenderClassFullName))
                {
                    string assemblyName =
                        string.IsNullOrEmpty(info.AssemblyName) ?
                            Assembly.GetCallingAssembly().GetName().FullName : info.AssemblyName;

                    string typeName = Assembly.CreateQualifiedName(assemblyName, info.AppenderClassFullName);
                    Type? typeOfAppender = Type.GetType(typeName);

                    if (typeOfAppender != null)
                    {
                        object[] param = { info };

                        AppenderBase? appender =
                            Activator.CreateInstance(typeOfAppender, param) as AppenderBase;

                        if (appender != null)
                        {
                            appenders.Add(appender);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Trace.Write(e.StackTrace);
            }
        }

        return appenders;
    }

    /// <summary>
    /// リソース解放
    /// </summary>
    void ReleaseAppenders()
    {
        lock (s_hotConfLockObj)
        {
            foreach (IAppender appender in m_appenders)
            {
                appender.Close();
            }

            m_appenders.Clear();
        }
    }

    #region IDisposable 実装
    /// <summary>
    /// 重複コールテスト用
    /// </summary>
    bool m_isDisposed = false;

    /// <summary>
    /// リソースを開放する。
    /// </summary>
    /// <param name="disposing"></param>
    void Dispose(bool disposing)
    {
        if (!m_isDisposed)
        {
            if (disposing)
            {
                try
                {
                    // 出力スレッドを停止
                    Stop();

                    // リソースを開放
                    Release();
                }
                catch (Exception e)
                {
                    Trace.Write(e.StackTrace);
                }
            }

            m_isDisposed = true;
        }
    }

    /// <summary>
    /// リソースを開放する。
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }
    #endregion

    #region IHotConfigurable実装
    /// <summary>
    /// 実行中に設定の変更があれば適用する。
    /// </summary>
    /// <param name="newConfig">新しい設定情報</param>
    public void ApplyNewConfiguration(LoggerConfiguration newConfig)
    {
        // Appender情報を新しい設定より再構築
        ReleaseAppenders();

        lock (s_hotConfLockObj)
        {
            m_appenders = CreateAppenders(newConfig.Appenders ?? new List<AppenderInfo>());
        }
    }
    #endregion
}
