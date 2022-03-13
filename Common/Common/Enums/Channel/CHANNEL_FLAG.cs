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

namespace AzerothSharp.Common;

[Flags]
public enum CHANNEL_FLAG : byte
{
    // General                  0x18 = 0x10 | 0x08
    // Trade                    0x3C = 0x20 | 0x10 | 0x08 | 0x04
    // LocalDefence             0x18 = 0x10 | 0x08
    // GuildRecruitment         0x38 = 0x20 | 0x10 | 0x08
    // LookingForGroup          0x50 = 0x40 | 0x10

    CHANNEL_FLAG_NONE = 0x0,
    CHANNEL_FLAG_CUSTOM = 0x1,
    CHANNEL_FLAG_UNK1 = 0x2,
    CHANNEL_FLAG_TRADE = 0x4,
    CHANNEL_FLAG_NOT_LFG = 0x8,
    CHANNEL_FLAG_GENERAL = 0x10,
    CHANNEL_FLAG_CITY = 0x20,
    CHANNEL_FLAG_LFG = 0x40
}
