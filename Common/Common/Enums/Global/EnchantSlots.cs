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

public enum EnchantSlots : byte
{
    ENCHANTMENT_PERM = 0,
    ENCHANTMENT_TEMP = 1,
    ENCHANTMENT_BONUS = 2,
    MAX_INSPECT = 3,
    ENCHANTMENT_PROP_SLOT_1 = 3, // used with RandomSuffix
    ENCHANTMENT_PROP_SLOT_2 = 4, // used with RandomSuffix
    ENCHANTMENT_PROP_SLOT_3 = 5, // used with RandomSuffix and RandomProperty
    ENCHANTMENT_PROP_SLOT_4 = 6, // used with RandomProperty
    ENCHANTMENT_PROP_SLOT_5 = 7, // used with RandomProperty
    MAX_ENCHANTS = 8
}
