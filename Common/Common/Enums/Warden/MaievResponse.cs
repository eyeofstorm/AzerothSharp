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

public enum MaievResponse : byte
{
    MAIEV_RESPONSE_FAILED_OR_MISSING = 0x0,          // The module was either currupt or not in the cache request transfer
    MAIEV_RESPONSE_SUCCESS = 0x1,                    // The module was in the cache and loaded successfully
    MAIEV_RESPONSE_RESULT = 0x2,
    MAIEV_RESPONSE_HASH = 0x4
}
