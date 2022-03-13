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

using AzerothSharp.Common;

namespace AzerothSharp.Auth.Network;

public struct AUTH_LOGON_PROOF_C
{
    public byte     Command         { get; set; } = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
    public byte[]   ClientPublicKey { get; set; } = new byte[32];
    public byte[]   ClientM1        { get; set; } = new byte[20];
    public byte[]   CrcHash         { get; set; } = new byte[20];
    public int      NumberOfKeys    { get; set; }
    public byte     SecurityFlags   { get; set; } = 0x0;
}
