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
using System.Security.Cryptography;
using System.Text;

namespace AzerothSharp.Logging;

/// <summary>
/// ファイルアクセス用ヘルプクラス。
/// </summary>
internal static class LogFileHelper
{
    /// <summary>
    /// ファイルパスのハッシュ値を計算する。
    /// </summary>
    /// <param name="fileFullPath">ファイルパス</param>
    /// <returns>ハッシュ値</returns>
    internal static string GetFileNameHash(string fileFullPath)
    {
        // 文字列をbyte型配列に変換する
        byte[] data = Encoding.UTF8.GetBytes(fileFullPath);

        // または、次のようにもできる
        SHA1 sha1 = SHA1.Create();

        // ハッシュ値を計算する
        byte[] bs = sha1.ComputeHash(data);

        // リソースを解放する
        sha1.Clear();

        string result = BitConverter.ToString(bs).ToLower().Replace("-", "");

        return result;
    }

    /// <summary>
    /// ログファイルサイズを取得する。
    /// </summary>
    /// <param name="logMaxSizeConf"> ログファイルサイズ設定</param>
    internal static long GetLogFileSizeLong(string? logMaxSizeConf)
    {
        long logSize = Consts.LogFile.DEFAULT_LOG_MAX_FILE_SIZE;

        try
        {
            if (!long.TryParse(logMaxSizeConf, out logSize))
            {
                if (logMaxSizeConf?.EndsWith("m", true, System.Globalization.CultureInfo.InvariantCulture) ?? false)
                {
                    logMaxSizeConf = logMaxSizeConf?.TrimEnd('m', 'M');

                    if (long.TryParse(logMaxSizeConf, out logSize))
                    {
                        logSize = logSize * 1024 * 1024;
                    }
                }
            }
        }
        catch (Exception)
        {
            // Do nothing. 
        }

        return logSize;
    }

    /// <summary>
    /// ログファイルサイズを取得する。
    /// </summary>
    /// <param name="logMaxSizeConf"> ログファイルサイズ設定</param>
    internal static long GetRollingLogFileSizeLong(string? logMaxSizeConf)
    {
        long logSize = Consts.LogFile.DEFAULT_ROLLING_LOG_MAX_FILE_SIZE;

        try
        {
            if (!long.TryParse(logMaxSizeConf, out logSize))
            {
                if (logMaxSizeConf?.EndsWith("m", true, System.Globalization.CultureInfo.InvariantCulture) ?? false)
                {
                    logMaxSizeConf = logMaxSizeConf.TrimEnd('m', 'M');

                    if (long.TryParse(logMaxSizeConf, out logSize))
                    {
                        logSize = logSize * 1024 * 1024;
                    }
                }
            }
        }
        catch (Exception)
        {
            // Do nothing. 
        }

        return logSize;
    }

    /// <summary>
    /// Get current log file backup index.
    /// </summary>
    /// <param name="logFileInfo">information of the log file.</param>
    /// <returns></returns>
    internal static int GetCurrentBackupIndex(FileInfo logFileInfo)
    {
        int maxBackupIndex = 0;
        string pathSearchPattern = string.Format("{0}{1}", logFileInfo.Name, ".*");

        FileInfo[]? logFilesInfo =
            logFileInfo.Directory?.GetFiles(pathSearchPattern, SearchOption.TopDirectoryOnly);

        if (logFilesInfo != null)
        {
            foreach (FileInfo file in logFilesInfo)
            {

                int fileIndex;

                if (int.TryParse(file.Extension.Replace(".", ""), out fileIndex))
                {
                    maxBackupIndex = Math.Max(maxBackupIndex, fileIndex);
                }
            }
        }

        return maxBackupIndex;
    }

    /// <summary>
    /// ファイルをバックアップ
    /// </summary>
    /// <param name="fi">ファイル情報</param>
    /// <param name="maxBackupIndex">バックアップファイルの最大番号</param>
    internal static void RollFile(FileInfo fi, int maxBackupIndex)
    {
        string filename;
        int curBakIdx = GetCurrentBackupIndex(fi);

        if (curBakIdx >= maxBackupIndex)
        {

            curBakIdx--;

            filename = string.Format("{0}.{1}",
                                     fi.FullName,
                                     maxBackupIndex);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        // バックアップファイル移動
        for (int i = curBakIdx; i >= 1; i--)
        {

            string srcFilePath = string.Format("{0}.{1}", fi.FullName, i);
            string dstFilePath = string.Format("{0}.{1}", fi.FullName, i + 1);

            try
            {
                File.Move(srcFilePath, dstFilePath);
            }
            catch (Exception e)
            {
                Trace.Write(e.StackTrace);
                continue;
            }
        }

        // 自分自身をバックアップ
        filename = string.Format("{0}.{1}", fi.FullName, 1);
        File.Move(fi.FullName, filename);
    }

    /// <summary>
    /// ログ出力レベルを取得
    /// </summary>
    /// <param name="logLevelString">ログ出力レベル設定文字列</param>
    /// <returns>ログ出力レベル</returns>
    internal static LogLevel GetLogLevel(string logLevelString)
    {
        LogLevel logLevel;

        switch (logLevelString.ToUpper())
        {
            case "TRACE":
                logLevel = LogLevel.Trace;
                break;
            case "DEBUG":
                logLevel = LogLevel.Debug;
                break;
            case "INFO":
                logLevel = LogLevel.Info;
                break;
            case "WARN":
                logLevel = LogLevel.Warn;
                break;
            case "ERROR":
                logLevel = LogLevel.Error;
                break;
            case "FATAL":
                logLevel = LogLevel.Fatal;
                break;
            default:
                logLevel = LogLevel.Info;
                break;
        }

        return logLevel;
    }
}
