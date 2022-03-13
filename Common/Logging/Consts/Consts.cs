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

namespace AzerothSharp.Logging;

/// <summary>
/// 定数定義
/// </summary>
internal static class Consts
{
    /// <summary>
    /// エラーコードの定義
    /// </summary>
    internal static class ErrorCode
    {
        /// <summary>
        /// 正常
        /// </summary>
        internal static readonly int R_NORMAL = 0;

        /// <summary>
        /// ファイル読み込みエラー
        /// </summary>
        internal static readonly int R_FILEREAD_ERR = 2103;

        /// <summary>
        /// ファイル削除エラー
        /// </summary>
        internal static readonly int R_FILEDELETE_ERR = 2105;

        /// <summary>
        /// パラメータ（引数）エラー
        /// </summary>
        internal static readonly int R_ENV_PARA = 2501;

        /// <summary>
        /// システム例外エラー（予期せめエラー）
        /// </summary>
        internal static readonly int R_EXP_ERR = 2900;
    }

    /// <summary>
    /// ログファイルの定義
    /// </summary>
    internal static class LogFile
    {
        /// <summary>
        /// ログファイルサイズの上限
        /// </summary>
        internal static readonly long DEFAULT_LOG_MAX_FILE_SIZE = 30 * 1024 * 1024;

        /// <summary>
        /// ログファイルサイズの上限
        /// </summary>
        internal static readonly long DEFAULT_ROLLING_LOG_MAX_FILE_SIZE = 1024 * 1024;

        /// <summary>
        /// The number of files would be kept.
        /// </summary>
        internal static readonly int DEFAULT_MAX_BACKUP_INDEX = 30;
    }
}
