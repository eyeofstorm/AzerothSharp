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

public enum AuraFlags
{
    AFLAG_NONE = 0x0,
    AFLAG_VISIBLE = 0x1,
    AFLAG_EFF_INDEX_1 = 0x2,
    AFLAG_EFF_INDEX_2 = 0x4,
    AFLAG_NOT_GUID = 0x8,
    AFLAG_CANCELLABLE = 0x10,
    AFLAG_HAS_DURATION = 0x20,
    AFLAG_UNK2 = 0x40,
    AFLAG_NEGATIVE = 0x80,
    AFLAG_POSITIVE = 0x1F,
    AFLAG_MASK = 0xFF
}
