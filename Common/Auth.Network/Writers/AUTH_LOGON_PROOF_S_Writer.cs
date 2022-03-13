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

public class AUTH_LOGON_PROOF_S_Writer : IPacketWriter<AUTH_LOGON_PROOF_S>
{
    private ILogger m_logger;

    public AUTH_LOGON_PROOF_S_Writer(ILogger logger)
    {
        m_logger = logger;
    }

    public async ValueTask WriteAsync(ChannelWriter<byte> writer, AUTH_LOGON_PROOF_S proof)
    {
        await writer.WriteAsync(proof.Command);
        await writer.WriteAsync(proof.Error);
        await writer.WriteEnumerableAsync(proof.M2);
        await writer.WriteEnumerableAsync(BitConverter.GetBytes(proof.AccountFlags));
        await writer.WriteEnumerableAsync(BitConverter.GetBytes(proof.SurveyId));
        await writer.WriteEnumerableAsync(BitConverter.GetBytes(proof.LoginFlags));

        m_logger.Debug($"AUTH_LOGON_PROOF_S normal response sended.");
    }

    public async ValueTask WriteErrorAsync(
                                ChannelWriter<byte> writer,
                                AuthResult authResult)
    {
        await writer.WriteAsync((byte)AuthCMD.CMD_AUTH_LOGON_PROOF);
        await writer.WriteAsync((byte)authResult);
        await writer.WriteAsync(0);

        m_logger.Debug($"AUTH_LOGON_PROOF_S error response sended.");
    }
}
