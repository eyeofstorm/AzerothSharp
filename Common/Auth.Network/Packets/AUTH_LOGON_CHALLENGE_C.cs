/*
 *This file is part of the AzerothCore Project. See AUTHORS file for Copyright information
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

using AzerothSharp.Common;

namespace AzerothSharp.Auth.Network;

public struct AUTH_LOGON_CHALLENGE_C
{
    public const int PLATFORM_LENGTH = 4;
    public const int OS_LENGTH = 4;
    public const int COUNTRY_LENGTH = 4;

    public byte   Command           { get; set; } = (byte)AuthCMD.CMD_AUTH_LOGON_CHALLENGE;
    public byte   ErrorCode         { get; set; }
    public Int16  Length            { get; set; }
    public String GameName          { get; set; } = String.Empty;
    public byte   Version1          { get; set; }
    public byte   Version2          { get; set; }
    public byte   Version3          { get; set; }
    public UInt16  ClientBuild      { get; set; }
    public byte[] Platform          { get; set; } = new byte[PLATFORM_LENGTH];
    public byte[] OS                { get; set; } = new byte[OS_LENGTH];
    public byte[] Country           { get; set; } = new byte[COUNTRY_LENGTH];
    public Int32  TimeZoneBias      { get; set; }
    public Int32  IpAddress         { get; set; }
    public byte   AccountNameLength { get; set; }
    public string AccountName       { get; set; } = String.Empty;
}
