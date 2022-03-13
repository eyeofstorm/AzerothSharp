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

public enum CheckTypes : byte
{
    MEM_CHECK = 0,
    PAGE_CHECK_A_B = 1,
    MPQ_CHECK = 2,
    LUA_STR_CHECK = 3,
    DRIVER_CHECK = 4,
    TIMING_CHECK = 5,
    PROC_CHECK = 6,
    MODULE_CHECK = 7
}
