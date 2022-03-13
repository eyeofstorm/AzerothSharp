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

public enum DynamicFlags   // Dynamic flags for units
{
    // Unit has blinking stars effect showing lootable
    UNIT_DYNFLAG_LOOTABLE = 0x1,

    // Shows marked unit as small red dot on radar
    UNIT_DYNFLAG_TRACK_UNIT = 0x2,

    // Gray mob title marks that mob is tagged by another player
    UNIT_DYNFLAG_OTHER_TAGGER = 0x4,

    // Blocks player character from moving
    UNIT_DYNFLAG_ROOTED = 0x8,

    // Shows infos like Damage and Health of the enemy
    UNIT_DYNFLAG_SPECIALINFO = 0x10,

    // Unit falls on the ground and shows like dead
    UNIT_DYNFLAG_DEAD = 0x20
}
