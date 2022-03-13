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
/// ログ書き込み
/// </summary>
public interface ILogger
{
    /// <summary>
    /// トレースログ出力可否フラグ
    /// </summary>
    bool IsTraceEnabled { get; }

    /// <summary>
    /// デバッグログ出力可否フラグ
    /// </summary>
    bool IsDebugEnabled { get; }

    /// <summary>
    /// エラーログ出力可否フラグ
    /// </summary>
    bool IsErrorEnabled { get; }

    /// <summary>
    /// 厳重エラーログ出力可否フラグ
    /// </summary>
    bool IsFatalEnabled { get; }

    /// <summary>
    /// 情報ログ出力可否フラグ
    /// </summary>
    bool IsInfoEnabled { get; }

    /// <summary>
    /// 警告ログ出力可否フラグ
    /// </summary>
    bool IsWarnEnabled { get; }

    /// <summary>
    /// トレースログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Trace(string message);

    /// <summary>
    /// トレースログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Trace(Exception e);

    /// <summary>
    /// デバッグログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Debug(string message);

    /// <summary>
    /// デバッグログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Debug(Exception e);

    /// <summary>
    /// エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Error(string message);

    /// <summary>
    /// エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Error(Exception e);

    /// <summary>
    /// 厳重エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Fatal(string message);

    /// <summary>
    /// 厳重エラーログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Fatal(Exception e);

    /// <summary>
    /// 情報ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Info(string message);

    /// <summary>
    /// 情報ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Info(Exception e);

    /// <summary>
    /// 警告ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Warn(string message);

    /// <summary>
    /// 警告ログを出力する。
    /// </summary>
    /// <param name="message">ログ内容</param>
    void Warn(Exception e);
}
