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

public enum ITEM_CLASS : byte
{
    ITEM_CLASS_CONSUMABLE = 0,
    ITEM_CLASS_CONTAINER = 1,
    ITEM_CLASS_WEAPON = 2,
    ITEM_CLASS_JEWELRY = 3,
    ITEM_CLASS_ARMOR = 4,
    ITEM_CLASS_REAGENT = 5,
    ITEM_CLASS_PROJECTILE = 6,
    ITEM_CLASS_TRADE_GOODS = 7,
    ITEM_CLASS_GENERIC = 8,
    ITEM_CLASS_BOOK = 9,
    ITEM_CLASS_MONEY = 10,
    ITEM_CLASS_QUIVER = 11,
    ITEM_CLASS_QUEST = 12,
    ITEM_CLASS_KEY = 13,
    ITEM_CLASS_PERMANENT = 14,
    ITEM_CLASS_JUNK = 15
}
