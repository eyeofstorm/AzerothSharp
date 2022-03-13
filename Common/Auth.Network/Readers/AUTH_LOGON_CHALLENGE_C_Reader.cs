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
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using AzerothSharp.Auth;
using AzerothSharp.Common;
using AzerothSharp.Network.Tcp;

namespace AzerothSharp.Auth.Network;

public class AUTH_LOGON_CHALLENGE_C_Reader : IPacketReader<AUTH_LOGON_CHALLENGE_C>
{
    public async ValueTask<AUTH_LOGON_CHALLENGE_C> ReadAsync(ChannelReader<byte> reader)
    {
        AUTH_LOGON_CHALLENGE_C request = new();

        request.Command = (byte)AuthCMD.CMD_AUTH_LOGON_CHALLENGE;
        request.ErrorCode = await reader.ReadAsync();
        request.Length = BitConverter.ToInt16(await reader.ReadArrayAsync(2));
        request.GameName = Encoding.UTF8.GetString(await reader.ReadArrayAsync(4));
        request.Version1 = await reader.ReadAsync();
        request.Version2 = await reader.ReadAsync();
        request.Version3 = await reader.ReadAsync();
        request.ClientBuild = BitConverter.ToUInt16(await reader.ReadArrayAsync(2));
        request.Platform = await reader.ReadArrayAsync(AUTH_LOGON_CHALLENGE_C.PLATFORM_LENGTH);
        request.OS = await reader.ReadArrayAsync(AUTH_LOGON_CHALLENGE_C.OS_LENGTH);
        request.Country = await reader.ReadArrayAsync(AUTH_LOGON_CHALLENGE_C.COUNTRY_LENGTH);
        request.TimeZoneBias = BitConverter.ToInt32(await reader.ReadArrayAsync(4));
        request.IpAddress = BitConverter.ToInt32(await reader.ReadArrayAsync(4));
        request.AccountNameLength = await reader.ReadAsync();
        request.AccountName = Encoding.UTF8.GetString(await reader.ReadArrayAsync(request.AccountNameLength));

        return request;
    }
}
