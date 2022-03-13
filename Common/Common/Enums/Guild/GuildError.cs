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

namespace AzerothSharp.Common;

public enum GuildError : byte
{
    GUILD_PLAYER_NO_MORE_IN_GUILD = 0x0,
    GUILD_INTERNAL = 0x1,
    GUILD_ALREADY_IN_GUILD = 0x2,
    ALREADY_IN_GUILD = 0x3,
    INVITED_TO_GUILD = 0x4,
    ALREADY_INVITED_TO_GUILD = 0x5,
    GUILD_NAME_INVALID = 0x6,
    GUILD_NAME_EXISTS = 0x7,
    GUILD_LEADER_LEAVE = 0x8,
    GUILD_PERMISSIONS = 0x8,
    GUILD_PLAYER_NOT_IN_GUILD = 0x9,
    GUILD_PLAYER_NOT_IN_GUILD_S = 0xA,
    GUILD_PLAYER_NOT_FOUND = 0xB,
    GUILD_NOT_ALLIED = 0xC
}
