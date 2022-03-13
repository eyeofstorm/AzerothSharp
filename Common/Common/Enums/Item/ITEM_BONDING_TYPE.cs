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

public enum ITEM_BONDING_TYPE : byte
{
    NO_BIND = 0,
    BIND_WHEN_PICKED_UP = 1,
    BIND_WHEN_EQUIPED = 2,
    BIND_WHEN_USED = 3,
    BIND_UNK_QUESTITEM1 = 4,
    BIND_UNK_QUESTITEM2 = 5
}
