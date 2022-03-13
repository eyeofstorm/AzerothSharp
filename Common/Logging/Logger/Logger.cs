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
using System.Diagnostics;
using System.Text;

namespace AzerothSharp.Logging;

/// <summary>
/// ログ書き込み用ヘルプクラスである
/// </summary>
public sealed class Logger : ILogger, IDisposable
{
    /// <summary>
    /// ログ出力レベル
    /// </summary>
    LogLevel m_rootLogLevel;

    /// <summary>
    /// ログ出力スレッド
    /// </summary>
    readonly LoggerThread m_loggerThread;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Logger()
    {
        LoggerSettings settings = LoggerSettings.GetInstance();

        m_rootLogLevel = LogFileHelper.GetLogLevel(settings.Configuration?.Logger?.Level?.Trim() ?? "INFO");

        m_loggerThread = LoggerThread.GetInstance();

        settings.RegisteLoggerSettingsChangedEventHandler(LoggerSettingsChanged);
    }

    /// <summary>
    /// ログ設定変更を適用する。
    /// </summary>
    /// <param name="sender">送信元</param>
    /// <param name="args">イベントパラメーター</param>
    private void LoggerSettingsChanged(object sender, LoggerSettingsChangedEventArgs args)
    {
        // Loggerの設定変更を適用
        m_rootLogLevel = LogFileHelper.GetLogLevel(args.NewConfigration.Logger?.Level?.Trim() ?? "INFO");

        // Appenderの設定変更を適用
        (m_loggerThread as IHotConfigurable).ApplyNewConfiguration(args.NewConfigration);
    }

    /// <summary>
    /// ログ出力
    /// </summary>
    /// <param name="level">ログ出力レベル</param>
    /// <param name="message">ログ内容</param>
    void AppendLog(LogLevel level, string message)
    {
        LogItem item = new LogItem
        {
            LogDateTime = DateTime.Now,
            LogLevel = level,
            Message = message,
            Caller = new StackFrame(2)
        };

        m_loggerThread.EnqueueLogItem(item);
    }

    #region ILogger 実装
    /// <summary>
    /// トレースログ出力可否フラグ
    /// </summary>
    public bool IsTraceEnabled
    {
        get { return (int)m_rootLogLevel >= (int)LogLevel.Trace; }
    }

    /// <summary>
    /// デバッグログ出力可否フラグ
    /// </summary>
    public bool IsDebugEnabled
    {
        get { return (int)m_rootLogLevel >= (int)LogLevel.Debug; }
    }

    /// <summary>
    /// 厳重エラーログ出力可否フラグ
    /// </summary>
    public bool IsFatalEnabled
    {
        get { return (int)m_rootLogLevel >= (int)LogLevel.Fatal; }
    }

    /// <summary>
    /// エラーログ出力可否フラグ
    /// </summary>
    public bool IsErrorEnabled
    {
        get
        {
            return (int)m_rootLogLevel >= (int)LogLevel.Error;
        }
    }

    /// <summary>
    /// 警告ログ出力可否フラグ
    /// </summary>
    public bool IsWarnEnabled
    {
        get { return (int)m_rootLogLevel >= (int)LogLevel.Warn; }
    }

    /// <summary>
    /// 情報ログ出力可否フラグ
    /// </summary>
    public bool IsInfoEnabled
    {
        get
        {
            return m_rootLogLevel >= (int)LogLevel.Info;
        }
    }

    /// <summary>
    /// トレースログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Trace(string message)
    {
        if (IsTraceEnabled)
        {
            AppendLog(LogLevel.Trace, message);
        }
    }

    /// <summary>
    /// トレースログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Trace(Exception e)
    {
        if (IsTraceEnabled)
        {
            StringBuilder logMsg = new();

            logMsg
                .AppendLine(e.Message)
                .AppendLine(e.StackTrace);

            if (e.InnerException is not null)
            {
                logMsg
                    .AppendLine(e.InnerException.Message)
                    .AppendLine(e.InnerException.StackTrace);
            }

            AppendLog(LogLevel.Trace, logMsg.ToString());
        }
    }

    /// <summary>
    /// デバッグログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Debug(string message)
    {
        if (IsDebugEnabled)
        {
            AppendLog(LogLevel.Debug, message);
        }
    }

    /// <summary>
    /// デバッグログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Debug(Exception e)
    {
        if (IsDebugEnabled)
        {
            StringBuilder logMsg = new();

            logMsg
                .AppendLine(e.Message)
                .AppendLine(e.StackTrace);

            if (e.InnerException is not null)
            {
                logMsg
                    .AppendLine(e.InnerException.Message)
                    .AppendLine(e.InnerException.StackTrace);
            }

            AppendLog(LogLevel.Debug, logMsg.ToString());
        }
    }

    /// <summary>
    /// エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Error(string message)
    {
        if (IsErrorEnabled)
        {
            AppendLog(LogLevel.Error, message);
        }
    }

    /// <summary>
    /// エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Error(Exception e)
    {
        if (IsErrorEnabled)
        {
            StringBuilder logMsg = new();

            logMsg
                .AppendLine(e.Message)
                .AppendLine(e.StackTrace);

            if (e.InnerException is not null)
            {
                logMsg
                    .AppendLine(e.InnerException.Message)
                    .AppendLine(e.InnerException.StackTrace);
            }

            AppendLog(LogLevel.Error, logMsg.ToString());
        }
    }

    /// <summary>
    /// 厳重エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Fatal(string message)
    {
        if (IsFatalEnabled)
        {
            AppendLog(LogLevel.Fatal, message);
        }
    }

    /// <summary>
    /// 厳重エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Fatal(Exception e)
    {
        if (IsFatalEnabled)
        {
            StringBuilder logMsg = new();

            logMsg
                .AppendLine(e.Message)
                .AppendLine(e.StackTrace);

            if (e.InnerException is not null)
            {
                logMsg
                    .AppendLine(e.InnerException.Message)
                    .AppendLine(e.InnerException.StackTrace);
            }

            AppendLog(LogLevel.Fatal, logMsg.ToString());
        }
    }

    /// <summary>
    /// 情報ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Info(string message)
    {
        if (IsInfoEnabled)
        {
            AppendLog(LogLevel.Info, message);
        }
    }

    /// <summary>
    /// 情報ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Info(Exception e)
    {
        if (IsInfoEnabled)
        {
            StringBuilder logMsg = new();

            logMsg
                .AppendLine(e.Message)
                .AppendLine(e.StackTrace);

            if (e.InnerException is not null)
            {
                logMsg
                    .AppendLine(e.InnerException.Message)
                    .AppendLine(e.InnerException.StackTrace);
            }

            AppendLog(LogLevel.Info, logMsg.ToString());
        }
    }

    /// <summary>
    /// 警告ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Warn(string message)
    {
        if (IsWarnEnabled)
        {
            AppendLog(LogLevel.Warn, message);
        }
    }

    /// <summary>
    /// 警告ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    public void Warn(Exception e)
    {
        if (IsWarnEnabled)
        {
            StringBuilder logMsg = new();

            logMsg
                .AppendLine(e.Message)
                .AppendLine(e.StackTrace);

            if (e.InnerException is not null)
            {
                logMsg
                    .AppendLine(e.InnerException.Message)
                    .AppendLine(e.InnerException.StackTrace);
            }

            AppendLog(LogLevel.Warn, logMsg.ToString());
        }
    }
    #endregion

    #region IDisposable 実装
    /// <summary>
    /// 重複コールテスト用
    /// </summary>
    bool m_isDisposed = false;

    /// <summary>
    /// リソースを開放する。
    /// </summary>
    /// <param name="disposing">リソースが開放中であるか</param>
    void Dispose(bool disposing)
    {
        if (!m_isDisposed)
        {
            if (disposing)
            {
                m_loggerThread.Dispose();
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
}
