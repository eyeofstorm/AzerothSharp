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
using AzerothSharp.Cryptography;

namespace AzerothSharp.Auth.Network;

public struct AUTH_LOGON_CHALLENGE_S
{
    public const int SALTLENGTH = 32;
    public const int EPHEMERAL_KEY_LENGTH = 32;

    public byte   Command         { get; set; } = (byte)AuthCMD.CMD_AUTH_LOGON_CHALLENGE;
    public byte   ErrorCode       { get; set; } = 0x0;
    public byte[] ServerPublicKey { get; set; } = new byte[EPHEMERAL_KEY_LENGTH];
    public byte[] Generator       { get; set; } = new byte[1];
    public byte[] SafePrime       { get; set; } = new byte[EPHEMERAL_KEY_LENGTH];
    public byte[] Salt            { get; set; } = new byte[SALTLENGTH];

    public AUTH_LOGON_CHALLENGE_S(SrpInteger serverPublicKey, SrpInteger generator, SrpInteger safePrime, SrpInteger salt)
    {
        Array.Copy(serverPublicKey.ToByteArray().Reverse().ToArray(), ServerPublicKey, EPHEMERAL_KEY_LENGTH);
        Array.Copy(generator.ToByteArray().Reverse().ToArray(), Generator, 1);
        Array.Copy(safePrime.ToByteArray().Reverse().ToArray(), SafePrime, EPHEMERAL_KEY_LENGTH);
        Array.Copy(salt.ToByteArray().Reverse().ToArray(), Salt, SALTLENGTH);
    }
}
