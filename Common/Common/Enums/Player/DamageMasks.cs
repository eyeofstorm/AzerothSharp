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
public enum DamageMasks
{
    DMG_NORMAL = 0x0,
    DMG_PHYSICAL = 0x1,
    DMG_HOLY = 0x2,
    DMG_FIRE = 0x4,
    DMG_NATURE = 0x8,
    DMG_FROST = 0x10,
    DMG_SHADOW = 0x20,
    DMG_ARCANE = 0x40
}
