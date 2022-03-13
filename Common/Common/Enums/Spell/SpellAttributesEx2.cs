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

public enum SpellAttributesEx2
{
    SPELL_ATTR_EX2_AUTO_SHOOT = 0x20, // Auto Shoot?
    SPELL_ATTR_EX2_HEALTH_FUNNEL = 0x800, // Health funnel pets?
    SPELL_ATTR_EX2_NOT_NEED_SHAPESHIFT = 0x80000, // does not necessarly need shapeshift
    SPELL_ATTR_EX2_CANT_CRIT = 0x20000000 // Spell can't crit
}
