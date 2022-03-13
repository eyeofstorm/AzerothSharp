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

using System;

namespace AzerothSharp.Common;

[Flags]
public enum RealmFlags : byte
{
    REALM_FLAG_NONE = 0x00,
    REALM_FLAG_VERSION_MISMATCH = 0x01,
    REALM_FLAG_OFFLINE = 0x02,
    REALM_FLAG_SPECIFYBUILD = 0x04,
    REALM_FLAG_UNK1 = 0x08,
    REALM_FLAG_UNK2 = 0x10,
    REALM_FLAG_RECOMMENDED = 0x20,
    REALM_FLAG_NEW = 0x40,
    REALM_FLAG_FULL = 0x80
}
