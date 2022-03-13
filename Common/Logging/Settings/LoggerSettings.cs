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
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace AzerothSharp.Logging;

/// <summary>
/// Appender定義
/// </summary>
public sealed class AppenderInfo
{
    /// <summary>
    /// 名前
    /// </summary>
    [XmlAttribute("name")]
    public string? AppenderName { get; set; }

    /// <summary>
    /// 出力レベル
    /// </summary>
    [XmlAttribute("level")]
    public string? Level { get; set; }

    /// <summary>
    /// クラスの完全限定名
    /// </summary>
    [XmlAttribute("class")]
    public string? AppenderClassFullName { get; set; }

    /// <summary>
    /// Assembly 名
    /// </summary>
    [XmlAttribute("assembly")]
    public string? AssemblyName { get; set; }

    /// <summary>
    /// ログファイルのパス
    /// </summary>
    [XmlElement("file")]
    public string? FilePath { get; set; }

    /// <summary>
    /// ログファイルの最大サイズ
    /// </summary>
    [XmlElement("max-file-size")]
    public string? MaxFileSize { get; set; }

    /// <summary>
    /// The number of files would be kept.
    /// </summary>
    [XmlElement("max-backup-index")]
    public string? MaxBackupIndex;

    /// <summary>
    /// 出力パターン
    /// </summary>
    [XmlElement("pattern")]
    public string? Pattern { get; set; }
}

/// <summary>
/// Appender参照定義
/// </summary>
public sealed class AppenderRefInfo
{
    /// <summary>
    /// Appenderの参照
    /// </summary>
    [XmlAttribute("ref")]
    public string? AppenderName { get; set; }
}

/// <summary>
/// ロガー定義
/// </summary>
public sealed class LoggerInfo
{
    /// <summary>
    /// 出力レベル
    /// </summary>
    [XmlAttribute("level")]
    public string? Level { get; set; }

    [XmlElement("appender-ref")]
    public AppenderRefInfo[]? AppenderRefs { get; set; }
}

/// <summary>
/// ログ出力設定情報
/// </summary>
[XmlRoot("configuration")]
public sealed class LoggerConfiguration
{
    /// <summary>
    /// ロック用オブジェクト
    /// </summary>
    private static readonly object s_lockObj = new object();

    /// <summary>
    /// デフォルト設定
    /// </summary>
    private static LoggerConfiguration? s_defaultValue;

    /// <summary>
    /// Appender設定
    /// </summary>
    [XmlElement("appender")]
    public List<AppenderInfo>? Appenders { get; set; }

    /// <summary>
    /// Logger設定
    /// </summary>
    [XmlElement("logger")]
    public LoggerInfo? Logger { get; set; }

    /// <summary>
    /// デフォルト値
    /// </summary>
    public static LoggerConfiguration DefaultValue
    {
        get
        {
            if (s_defaultValue == null)
            {
                lock (s_lockObj)
                {
                    if (s_defaultValue == null)
                    {
                        s_defaultValue = new LoggerConfiguration();

                        s_defaultValue.Logger = new LoggerInfo()
                        {
                            Level = "INFO",

                            AppenderRefs = new AppenderRefInfo[]
                            {
                                new AppenderRefInfo()
                                {
                                    AppenderName = "STDOUT"
                                }
                            }
                        };

                        s_defaultValue.Appenders = new List<AppenderInfo>();

                        AppenderInfo stdoutAppender = new AppenderInfo()
                        {
                            AppenderName = "STDOUT",
                            Level = "INFO",
                            AppenderClassFullName = "AzerothCoreSharp.Common.Logging.ConsoleAppender",
                            Pattern = "%d{yyyy/MM/dd HH:mm:ss.fff} [%level] - %message %newline"
                        };

                        s_defaultValue.Appenders.Add(stdoutAppender);
                    }
                }
            }

            return s_defaultValue;
        }
    }
}

/// <summary>
/// ログ設定変更イベント処理用のデリゲート
/// </summary>
/// <param name="sender">送信元</param>
/// <param name="args">イベントパラメーター</param>
internal delegate void LoggerSettingsChangedEventHandler(object sender, LoggerSettingsChangedEventArgs args);

/// <summary>
/// ログ出力設定情報の読込み
/// </summary>
internal sealed class LoggerSettings : IDisposable
{
    /// <summary>
    /// ロック用
    /// </summary>
    private static readonly object s_lockObj = new object();

    /// <summary>
    /// このクラスのインスタンス
    /// </summary>
    private static volatile LoggerSettings? s_instance;

    /// <summary>
    /// 設定ＸＭＬファイル読み込み用
    /// </summary>
    private readonly SettingsXmlLoador<LoggerConfiguration> m_loador = new SettingsXmlLoador<LoggerConfiguration>();

    /// <summary>
    /// ログ設定情報
    /// </summary>
    private LoggerConfiguration? m_configuration;

    /// <summary>
    /// ログ設定 XML ファイル監視
    /// </summary>
    private FileSystemWatcher m_settingsXmlFileWatcher = new FileSystemWatcher();

    /// <summary>
    /// ログ設定情報を取得する。
    /// </summary>
    internal LoggerConfiguration? Configuration
    {
        get
        {
            lock (s_lockObj)
            {
                return m_configuration;
            }
        }

        set
        {
            lock (s_lockObj)
            {
                m_configuration = value;
            }
        }
    }

    /// <summary>
    /// ログ設定変更イベント
    /// </summary>
    internal event LoggerSettingsChangedEventHandler? LoggerSettingsChanged;

    /// <summary>
    /// インスタンス取得
    /// </summary>
    /// <returns>このクラスのインスタンス</returns>
    internal static LoggerSettings GetInstance()
    {
        if (s_instance == null)
        {
            lock (s_lockObj)
            {
                if (s_instance == null)
                {
                    s_instance = new LoggerSettings();
                }
            }
        }

        return s_instance;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    LoggerSettings()
    {
        try
        {
            // 設定ファイルから設定情報を読み込み
            string? workDir = Assembly.GetExecutingAssembly().Location;
            string fullPath = string.Empty;
            FileInfo fi = new FileInfo(workDir);
            string confFileName = "Logger.xml";

            workDir = fi.Directory?.FullName;
            fullPath = Path.Combine(workDir ?? "", confFileName);

            m_loador.Load(fullPath);
            m_configuration = m_loador.Data;

            // 設定ファイル変更監視の設定
            m_settingsXmlFileWatcher.Path = workDir ?? ".";
            m_settingsXmlFileWatcher.Filter = confFileName;
            m_settingsXmlFileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.Size;
            m_settingsXmlFileWatcher.Changed += new FileSystemEventHandler(OnLoggerSettingsXmlFileChanged);
            m_settingsXmlFileWatcher.EnableRaisingEvents = true;
        }
        catch (Exception e)
        {
            Trace.Write(e.StackTrace);
            Configuration = LoggerConfiguration.DefaultValue;
        }
    }

    /// <summary>
    /// ログ設定変更イベントの処理用関数を登録
    /// </summary>
    /// <param name="handler">ログ設定変更イベントの処理用関数</param>
    internal void RegisteLoggerSettingsChangedEventHandler(LoggerSettingsChangedEventHandler handler)
    {
        LoggerSettingsChanged += handler;
    }

    /// <summary>
    /// ログ設定ファイル変更イベントを処理する。
    /// </summary>
    /// <param name="sender">送信元</param>
    /// <param name="e">イベントパラメーター</param>
    private void OnLoggerSettingsXmlFileChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            m_loador.Reload();
            Configuration = m_loador.Data;

        }
        catch (Exception ex)
        {

            Trace.Write(ex.StackTrace);
            Configuration = LoggerConfiguration.DefaultValue;
        }

        if (LoggerSettingsChanged != null)
        {
            LoggerSettingsChanged(this, new LoggerSettingsChangedEventArgs(Configuration ?? LoggerConfiguration.DefaultValue));
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
    /// <param name="disposing">リソースが開放中であるか</param>
    void Dispose(bool disposing)
    {
        if (!m_isDisposed)
        {

            if (disposing)
            {
                m_settingsXmlFileWatcher.Dispose();
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
