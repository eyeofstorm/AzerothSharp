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

namespace AzerothSharp.Auth;

public struct BuildInfoEntity
{
    public UInt16 build;
    public UInt32 majorVersion;
    public UInt32 minorVersion;
    public UInt32 bugfixVersion;
    public String hotfixVersion;
    public String winAuthSeed;
    public String win64AuthSeed;
    public String mac64AuthSeed;
    public String winChecksumSeed;
    public String macChecksumSeed;
}
