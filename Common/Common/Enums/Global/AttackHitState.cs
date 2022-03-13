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

public enum AttackHitState
{
    HIT_UNARMED = HITINFO_NORMALSWING,
    HIT_NORMAL = HITINFO_HITANIMATION,
    HIT_NORMAL_OFFHAND = HITINFO_HITANIMATION + HITINFO_LEFTSWING,
    HIT_MISS = HITINFO_MISS,
    HIT_MISS_OFFHAND = HITINFO_MISS + HITINFO_LEFTSWING,
    HIT_CRIT = HITINFO_CRITICALHIT,
    HIT_CRIT_OFFHAND = HITINFO_CRITICALHIT + HITINFO_LEFTSWING,
    HIT_RESIST = HITINFO_RESIST,
    HIT_CRUSHING_BLOW = HITINFO_CRUSHING,
    HIT_GLANCING_BLOW = HITINFO_GLANCING,
    HITINFO_NORMALSWING = 0x0,
    HITINFO_UNK = 0x1,
    HITINFO_HITANIMATION = 0x2,
    HITINFO_LEFTSWING = 0x4,
    HITINFO_RANGED = 0x8,
    HITINFO_MISS = 0x10,
    HITINFO_ABSORB = 0x20,
    HITINFO_RESIST = 0x40,
    HITINFO_UNK2 = 0x100,
    HITINFO_CRITICALHIT = 0x200,
    HITINFO_BLOCK = 0x800,
    HITINFO_UNK3 = 0x2000,
    HITINFO_CRUSHING = 0x8000,
    HITINFO_GLANCING = 0x10000,
    HITINFO_NOACTION = 0x10000,
    HITINFO_SWINGNOHITSOUND = 0x80000
}
