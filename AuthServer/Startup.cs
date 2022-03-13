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

using AzerothSharp.Auth.Configuration;
using AzerothSharp.Auth.Storage;
using AzerothSharp.Common;
using AzerothSharp.Configuration;
using AzerothSharp.Logging;
using AzerothSharp.Network.Tcp;
using AzerothSharp.Shared;

namespace AzerothSharp.AuthServer;

public class Startup
{
    private readonly ILogger m_logger;
    private readonly IAuthStorage m_authStorage;
    private readonly RealmList m_realmList;
    private readonly XmlConfigurationProvider<AuthServerConfiguration> m_configurationProvider;
    private readonly TcpServer m_tcpServer;

    public Startup(
                ILogger logger,
                IAuthStorage authStorage,
                XmlConfigurationProvider<AuthServerConfiguration> configurationProvider,
                RealmList realmList,
                TcpServer tcpServer)
	{
        m_logger = logger;
        m_authStorage = authStorage;
        m_realmList = realmList;
        m_configurationProvider = configurationProvider;
        m_tcpServer = tcpServer;
	}

    public async Task StartAsync()
    {
        Banner.Show();

        LoadConfiguration();

        await ConnectToDatabaseAsync().ConfigureAwait(false);

        await m_realmList.InitializeAsync(20);

        StartTcpServer();
    }

    private void LoadConfiguration()
    {
        m_configurationProvider.LoadFromFile("etc/AuthServer.xml");
        m_logger.Info("Realm configuration has been loaded");
    }

    private async Task ConnectToDatabaseAsync()
    {
        await m_authStorage.ConnectAsync(
                                m_authStorage,
                                m_configurationProvider.GetConfiguration().AccountConnectionString);

        m_logger.Info("Connection to account database has been established");
    }

    private void StartTcpServer()
    {
        var configuration = m_configurationProvider.GetConfiguration();
        IPEndPoint endpoint = IPEndPoint.Parse(configuration.AuthServerEndpoint);
        m_tcpServer.Start(endpoint, 1);

        m_logger.Info("Tcp server has been started");
    }
}
