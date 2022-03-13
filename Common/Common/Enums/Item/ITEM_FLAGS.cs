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
public enum ITEM_FLAGS
{
    ITEM_FLAGS_BINDED = 0x1,
    ITEM_FLAGS_CONJURED = 0x2,
    ITEM_FLAGS_OPENABLE = 0x4,
    ITEM_FLAGS_WRAPPED = 0x8,
    ITEM_FLAGS_WRAPPER = 0x200, // used or not used wrapper
    ITEM_FLAGS_PARTY_LOOT = 0x800, // determines if item is party loot or not
    ITEM_FLAGS_CHARTER = 0x2000, // arena/guild charter
    ITEM_FLAGS_THROWABLE = 0x400000, // not used in game for check trow possibility, only for item in game tooltip
    ITEM_FLAGS_SPECIALUSE = 0x800000
}
