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

public enum SpellAuraStates
{
    AURASTATE_FLAG_DODGE_BLOCK = 1,
    AURASTATE_FLAG_HEALTH20 = 2,
    AURASTATE_FLAG_BERSERK = 4,
    AURASTATE_FLAG_JUDGEMENT = 16,
    AURASTATE_FLAG_PARRY = 64,
    AURASTATE_FLAG_LASTKILLWITHHONOR = 512,
    AURASTATE_FLAG_CRITICAL = 1024,
    AURASTATE_FLAG_HEALTH35 = 4096,
    AURASTATE_FLAG_IMMOLATE = 8192,
    AURASTATE_FLAG_REJUVENATE = 16384,
    AURASTATE_FLAG_POISON = 32768
}
