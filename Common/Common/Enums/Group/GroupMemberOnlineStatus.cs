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

[Flags]
public enum GroupMemberOnlineStatus
{
    MEMBER_STATUS_OFFLINE = 0x0,
    MEMBER_STATUS_ONLINE = 0x1,
    MEMBER_STATUS_PVP = 0x2,
    MEMBER_STATUS_DEAD = 0x4,            // dead (health=0)
    MEMBER_STATUS_GHOST = 0x8,           // ghost (health=1)
    MEMBER_STATUS_PVP_FFA = 0x10,        // pvp ffa
    MEMBER_STATUS_UNK3 = 0x20,           // unknown
    MEMBER_STATUS_AFK = 0x40,            // afk flag
    MEMBER_STATUS_DND = 0x80            // dnd flag
}
