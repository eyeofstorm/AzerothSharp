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

public class AuthTcpClient : ITcpClient
{
    private readonly ILogger m_logger;
    private readonly AuthSession m_clientInfo;
    private readonly Router m_router;

    public AuthTcpClient(ILogger logger, Router router, AuthSession clientModel)
    {
        m_logger = logger;
        m_router = router;
        m_clientInfo = clientModel;
    }

    public async void HandleAsync(
                        ChannelReader<byte> reader,
                        ChannelWriter<byte> writer,
                        CancellationToken cancellationToken)
    {
        
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var opcode = await reader.ReadAsync(cancellationToken);
                var packetHandler = m_router.GetPacketHandler(opcode);

                if (packetHandler != null)
                {
                    string? opcodeName = Enum.GetName((AuthCMD)opcode);
                    m_logger.Warn($"opcode: {opcodeName} received!");

                    await packetHandler.HandleAsync(reader, writer, m_clientInfo);
                }
                else
                {
                    string opcodeName = Enum.GetName((AuthCMD)opcode) ?? $"0X{opcode:08X}";
                    m_logger.Warn($"opcode: {opcodeName} skiped!");
                }
            }
            catch (Exception ex)
            {
                m_logger.Error(ex);
            }
        }
        
    }
}
