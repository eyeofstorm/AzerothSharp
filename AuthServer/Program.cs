﻿/*
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

using Autofac;

using AzerothSharp.Common;

namespace AzerothSharp.AuthServer;

public class Program
{
    public static async Task Main(string[] args)
    {
        ContainerBuilder builder = new();

        builder
            .RegisterModule<LoggerModule>()
            .RegisterModule<ConfigurationModule>()
            .RegisterModule<StorageModule>()
            .RegisterModule<TcpServerModule>()
            .RegisterModule<CommonModule>()
            .RegisterModule<AuthModule>();

        var container = builder.Build();
        var startup = container.Resolve<Startup>();

        // start auth server service
        await startup.StartAsync();

        Thread.CurrentThread.Join();

        Console.WriteLine("AuthServer exited.");
    }
}
