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
using System.Net.Sockets;

using AzerothSharp.Logging;
using AzerothSharp.Network.Tcp;

namespace AzerothSharp.Auth.Network;

public class AuthTcpClientFactory : ITcpClientFactory
{
    private readonly ILogger m_logger;
    private readonly Router m_router;

    public AuthTcpClientFactory(ILogger logger, Router router)
    {
        m_router = router;
        m_logger = logger;
    }

    public Task<ITcpClient> CreateTcpClientAsync(Socket clientSocket)
    {
        if (clientSocket == null)
        {
            throw new ArgumentNullException(nameof(clientSocket));
        }

        IPEndPoint? ipEndPoint = (IPEndPoint?)clientSocket.RemoteEndPoint;

        if (ipEndPoint == null)
        {
            throw new ArgumentNullException(paramName: nameof(ipEndPoint));
        }

        return Task.FromResult<ITcpClient>(
                        new AuthTcpClient(
                                m_logger,
                                m_router,
                                new AuthSession(ipEndPoint)));
    }
}
