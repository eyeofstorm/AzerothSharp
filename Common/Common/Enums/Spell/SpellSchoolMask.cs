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

public enum SpellSchoolMask
{
    SPELL_SCHOOL_MASK_NONE = 0x0,
    SPELL_SCHOOL_MASK_NORMAL = 0x1,
    SPELL_SCHOOL_MASK_HOLY = 0x2,
    SPELL_SCHOOL_MASK_FIRE = 0x4,
    SPELL_SCHOOL_MASK_NATURE = 0x8,
    SPELL_SCHOOL_MASK_FROST = 0x10,
    SPELL_SCHOOL_MASK_SHADOW = 0x20,
    SPELL_SCHOOL_MASK_ARCANE = 0x40,
    SPELL_SCHOOL_MASK_SPELL = SPELL_SCHOOL_MASK_FIRE | SPELL_SCHOOL_MASK_NATURE | SPELL_SCHOOL_MASK_FROST | SPELL_SCHOOL_MASK_SHADOW | SPELL_SCHOOL_MASK_ARCANE,
    SPELL_SCHOOL_MASK_MAGIC = SPELL_SCHOOL_MASK_HOLY | SPELL_SCHOOL_MASK_SPELL,
    SPELL_SCHOOL_MASK_ALL = SPELL_SCHOOL_MASK_NORMAL | SPELL_SCHOOL_MASK_MAGIC
}
