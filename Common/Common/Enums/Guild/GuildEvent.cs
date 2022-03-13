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

public enum GuildEvent : byte
{
    PROMOTION = 0,           // uint8(2), string(name), string(rankName)
    DEMOTION = 1,            // uint8(2), string(name), string(rankName)
    MOTD = 2,                // uint8(1), string(text)                                             'Guild message of the day: <text>
    JOINED = 3,              // uint8(1), string(name)                                             '<name> has joined the guild.
    LEFT = 4,                // uint8(1), string(name)                                             '<name> has left the guild.
    REMOVED = 5,             // ??
    LEADER_IS = 6,           // uint8(1), string(name                                              '<name> is the leader of your guild.
    LEADER_CHANGED = 7,      // uint8(2), string(oldLeaderName), string(newLeaderName)
    DISBANDED = 8,           // uint8(0)                                                           'Your guild has been disbanded.
    TABARDCHANGE = 9,        // ??
    SIGNED_ON = 12,
    SIGNED_OFF = 13
}
