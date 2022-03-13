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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Channels;

using AzerothSharp.Logging;

namespace AzerothSharp.Network.Tcp;

public class TcpServer
{
    private readonly Logger m_logger;
    private readonly CancellationTokenSource m_cancellationTokenSource;
    private readonly ITcpClientFactory m_tcpClientFactory;
    private Socket? m_serverSocket;

    public TcpServer(ITcpClientFactory tcpClientFactory)
    {
        m_logger = LoggerFactory.GetLogger();
        m_cancellationTokenSource = new CancellationTokenSource();
        m_tcpClientFactory = tcpClientFactory;
    }

    public void Start(IPEndPoint endPoint, int backlog)
    {
        m_serverSocket = new Socket(
                                AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp);

        m_serverSocket.Bind(endPoint);
        m_serverSocket.Listen(backlog);

        StartAcceptLoop();
    }

    private async void StartAcceptLoop()
    {
        while (!m_cancellationTokenSource.IsCancellationRequested)
        {
            if (m_serverSocket != null)
            {
                Socket clientSocket = await m_serverSocket.AcceptAsync();

                m_logger.Debug("New Tcp conenction established");

                OnAcceptAsync(clientSocket);
            }
        }
    }

    private async void OnAcceptAsync(Socket clientSocket)
    {
        try
        {
            ITcpClient tcpClient =
                await m_tcpClientFactory.CreateTcpClientAsync(clientSocket);

            Channel<byte> recieveChannel = Channel.CreateUnbounded<byte>();
            Channel<byte> sendChannel = Channel.CreateUnbounded<byte>();

            RecieveAsync(clientSocket, recieveChannel.Writer);
            SendAsync(clientSocket, sendChannel.Reader);

            tcpClient.HandleAsync(
                        recieveChannel.Reader,
                        sendChannel.Writer,
                        m_cancellationTokenSource.Token);
        }
        catch (Exception e)
        {
            m_logger.Error(e);
        }
    }

    private async void RecieveAsync(Socket clientSocket, ChannelWriter<byte> writer)
    {
        try
        {
            var buffer = new byte[clientSocket.ReceiveBufferSize];

            while (!m_cancellationTokenSource.IsCancellationRequested)
            {
                var bytesRead = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);

                if (bytesRead == 0)
                {
                    clientSocket.Dispose();

                    m_logger.Debug("Tcp conenction closed");

                    return;
                }

                await writer.WriteArrayAsync(buffer, bytesRead);
            }
        }
        catch (Exception e)
        {
            m_logger.Error(e);
        }
    }

    private async void SendAsync(Socket client, ChannelReader<byte> reader)
    {
        try
        {
            var buffer = new byte[client.SendBufferSize];

            while (!m_cancellationTokenSource.IsCancellationRequested)
            {
                await reader.WaitToReadAsync();
                int writeCount;

                for (writeCount = 0;
                     writeCount < buffer.Length && reader.TryRead(out buffer[writeCount]);
                     writeCount++)
                {
                    ;
                }

                ArraySegment<byte> arraySegment = new(buffer, 0, writeCount);
                await client.SendAsync(arraySegment, SocketFlags.None);
            }
        }
        catch (Exception e)
        {
            m_logger.Error(e);
        }
    }
}
