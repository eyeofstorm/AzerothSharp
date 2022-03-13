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

public struct AUTH_LOGON_PROOF_S
{
    public byte   Command      { get; set; } = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF;
    public byte   Error        { get; set; }
    public byte[] M2           { get; set; } = Array.Empty<byte>();
    public UInt32 AccountFlags { get; set; }
    public UInt32 SurveyId     { get; set; }
    public UInt32 LoginFlags   { get; set; }
}
