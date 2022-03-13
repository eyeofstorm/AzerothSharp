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

using System.Threading.Channels;

using AzerothSharp.Common;
using AzerothSharp.Logging;
using AzerothSharp.Network.Tcp;

namespace AzerothSharp.Auth.Network;

public class AUTH_LOGON_CHALLENGE_S_Writer : IPacketWriter<AUTH_LOGON_CHALLENGE_S>
{

    private ILogger m_logger;

    public AUTH_LOGON_CHALLENGE_S_Writer(ILogger logger)
    {
        m_logger = logger;
    }

    public async ValueTask WriteAsync(
                                ChannelWriter<byte> writer,
                                AUTH_LOGON_CHALLENGE_S packet)
    {
        await writer.WriteAsync((byte)AuthCMD.CMD_AUTH_LOGON_CHALLENGE);
        await writer.WriteAsync((byte)AccountState.LOGIN_OK);
        await writer.WriteAsync((byte)AuthResult.WOW_SUCCESS);
        await writer.WriteEnumerableAsync(packet.ServerPublicKey);
        await writer.WriteAsync(1);
        await writer.WriteAsync(packet.Generator[0]);
        await writer.WriteAsync(32);
        await writer.WriteEnumerableAsync(packet.SafePrime);
        await writer.WriteEnumerableAsync(packet.Salt);
        await writer.WriteEnumerableAsync(new byte[] { 0xBA, 0xA3, 0x1E, 0x99, 0xA0, 0x0B, 0x21, 0x57, 0xFC, 0x37, 0x3F, 0xB3, 0x69, 0xCD, 0xD2, 0xF1 });
        await writer.WriteAsync(0);

        m_logger.Debug($"AUTH_LOGON_CHALLENGE_S normal response sended.");
    }

    public async ValueTask WriteErrorAsync(
                                ChannelWriter<byte> writer,
                                AuthResult authResult)
    {
        await writer.WriteAsync((byte)AuthCMD.CMD_AUTH_LOGON_CHALLENGE);
        await writer.WriteAsync((byte)AccountState.LOGIN_OK);
        await writer.WriteAsync((byte)authResult);

        m_logger.Debug($"AUTH_LOGON_CHALLENGE_S error response sended.");
    }
}
