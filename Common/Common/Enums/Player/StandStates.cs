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

public enum StandStates : byte
{
    STANDSTATE_STAND = 0,
    STANDSTATE_SIT = 1,
    STANDSTATE_SIT_CHAIR = 2,
    STANDSTATE_SLEEP = 3,
    STANDSTATE_SIT_LOW_CHAIR = 4,
    STANDSTATE_SIT_MEDIUM_CHAIR = 5,
    STANDSTATE_SIT_HIGH_CHAIR = 6,
    STANDSTATE_DEAD = 7,
    STANDSTATE_KNEEL = 8
}
