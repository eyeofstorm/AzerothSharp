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

namespace AzerothSharp.Logging;

/// <summary>
/// ログ設定変更イベントのパラメーター
/// </summary>
public class LoggerSettingsChangedEventArgs : EventArgs
{
    /// <summary>
    /// 新しい設定内容
    /// </summary>
    private LoggerConfiguration m_newConfigration;

    /// <summary>
    /// 新しい設定内容を取得する
    /// </summary>
    public LoggerConfiguration NewConfigration
    {
        get
        {
            return m_newConfigration;
        }
    }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="newConfigration">新しい設定</param>
    public LoggerSettingsChangedEventArgs(LoggerConfiguration newConfigration)
    {
        m_newConfigration = newConfigration;
    }
}
