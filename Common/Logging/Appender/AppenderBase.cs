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
using System.IO;

namespace AzerothSharp.Logging;

/// <summary>
/// ログ出力実装クラスの基底クラス
/// </summary>
public abstract class AppenderBase : IAppender
{
    /// <summary>
    /// 名前
    /// </summary>
    private string m_appenderName;

    /// <summary>
    /// ログ出力レベル
    /// </summary>
    private readonly LogLevel m_lowestLevel;

    /// <summary>
    /// レイアウト
    /// </summary>
    private readonly PatternLayout m_layout;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="appenderInfo">Appender info.</param>
    public AppenderBase(AppenderInfo? appenderInfo)
        : this()
    {
        m_appenderName = appenderInfo?.AppenderName ?? "";
        m_layout = new PatternLayout(appenderInfo?.Pattern ?? PatternLayout.DEFAULT_PATTERN);
        m_lowestLevel = LogFileHelper.GetLogLevel(appenderInfo?.Level ?? "INFO");
    }

    /// <summary>
    /// コンストラクター
    /// </summary>
    AppenderBase()
    {
        m_lowestLevel = LogLevel.Info;
        m_appenderName = string.Empty;
        m_layout = new PatternLayout(PatternLayout.DEFAULT_PATTERN);
    }

    /// <summary>
    /// コンストラクター
    /// </summary>
    AppenderBase(string appendName, PatternLayout patternLayout, LogLevel logLevel)
    {
        m_appenderName = appendName;
        m_layout = patternLayout;
        m_lowestLevel = logLevel;
    }

    /// <summary>
    /// Appends the log message.
    /// </summary>
    /// <param name="logItem">Log item.</param>
    protected abstract void AppendLogMessage(LogItem logItem);

    /// <summary>
    /// リソースを開放
    /// </summary>
    protected abstract void CloseAppender();

    /// <summary>
    /// ログメッセージの作成
    /// </summary>
    /// <param name="logItem">ログ内容</param>
    /// <returns></returns>
    protected void WriteFormattedLogMessage(StreamWriter writer, LogItem logItem)
    {
        try
        {
            string? logMsg = m_layout.FormatLogItem(logItem);

            if (string.IsNullOrEmpty(logMsg))
            {
                logMsg = logItem?.Message;
            }

            writer.Write(logMsg);
            writer.Flush();
        }
        catch (Exception e)
        {
            Trace.WriteLine(e.StackTrace);
        }
    }

    /// <summary>
    /// ログメッセージの作成
    /// </summary>
    /// <returns>The log message.</returns>
    /// <param name="logItem">Log item.</param>
    protected virtual void CreateLogMessage(StreamWriter writer, LogItem logItem)
    {
        WriteFormattedLogMessage(writer, logItem);
    }

    #region IAppender実装
    /// <summary>
    /// 名前を取得または設定する。
    /// </summary>
    public string Name
    {
        get
        {
            return m_appenderName;
        }

        set
        {
            m_appenderName = value;
        }
    }

    /// <summary>
    /// ログを出力する
    /// </summary>
    /// <param name="logItem">Log item.</param>
    public void Append(LogItem logItem)
    {
        try
        {
            if ((int)m_lowestLevel >= (int)logItem.LogLevel)
            {
                AppendLogMessage(logItem);
            }
        }
        catch
        {
            // 例外が発生しても対処の仕方がないので何もしない。
        }
    }

    /// <summary>
    /// リソースを開放
    /// </summary>
    public void Close()
    {
        try
        {
            CloseAppender();
        }
        catch
        {
            // 例外が発生しても対処の仕方がないので何もしない。
        }
    }
    #endregion
}
