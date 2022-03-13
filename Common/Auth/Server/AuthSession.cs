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

using System.Net;

using AzerothSharp.Cryptography;

namespace AzerothSharp.Auth;

public class AuthSession
{
    public IPEndPoint           RemoteEnpoint   { get; }
    public string               AccountName     { get; set; } = string.Empty;
    public string               OS              { get; set; } = String.Empty;
    public UInt16               ClientBuild     { get; set; }
    public UInt32               Expversion      { get; set; }
    public SrpServerAuth?       Srp6            { get; set; } = null;
    public AccountInfoEntity    AccountInfo     { get; set; }

    public AuthSession(IPEndPoint remoteEnpoint)
    {
        RemoteEnpoint = remoteEnpoint;
    }
}
