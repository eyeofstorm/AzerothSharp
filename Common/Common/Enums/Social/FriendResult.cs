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

public enum FriendResult : byte
{
    FRIEND_DB_ERROR = 0x0,
    FRIEND_LIST_FULL = 0x1,
    FRIEND_ONLINE = 0x2,
    FRIEND_OFFLINE = 0x3,
    FRIEND_NOT_FOUND = 0x4,
    FRIEND_REMOVED = 0x5,
    FRIEND_ADDED_ONLINE = 0x6,
    FRIEND_ADDED_OFFLINE = 0x7,
    FRIEND_ALREADY = 0x8,
    FRIEND_SELF = 0x9,
    FRIEND_ENEMY = 0xA,
    FRIEND_IGNORE_FULL = 0xB,
    FRIEND_IGNORE_SELF = 0xC,
    FRIEND_IGNORE_NOT_FOUND = 0xD,
    FRIEND_IGNORE_ALREADY = 0xE,
    FRIEND_IGNORE_ADDED = 0xF,
    FRIEND_IGNORE_REMOVED = 0x10
}
