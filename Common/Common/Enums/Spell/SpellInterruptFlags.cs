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

public enum SpellInterruptFlags
{
    SPELL_INTERRUPT_FLAG_MOVEMENT = 0x1, // why need this for instant?
    SPELL_INTERRUPT_FLAG_PUSH_BACK = 0x2, // push back
    SPELL_INTERRUPT_FLAG_INTERRUPT = 0x4, // interrupt
    SPELL_INTERRUPT_FLAG_AUTOATTACK = 0x8, // no
    SPELL_INTERRUPT_FLAG_DAMAGE = 0x10  // _complete_ interrupt on direct damage?
}
