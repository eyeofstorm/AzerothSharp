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

public enum SpellMissInfo : byte
{
    SPELL_MISS_NONE = 0,
    SPELL_MISS_MISS = 1,
    SPELL_MISS_RESIST = 2,
    SPELL_MISS_DODGE = 3,
    SPELL_MISS_PARRY = 4,
    SPELL_MISS_BLOCK = 5,
    SPELL_MISS_EVADE = 6,
    SPELL_MISS_IMMUNE = 7,
    SPELL_MISS_IMMUNE2 = 8,
    SPELL_MISS_DEFLECT = 9,
    SPELL_MISS_ABSORB = 10,
    SPELL_MISS_REFLECT = 11
}
