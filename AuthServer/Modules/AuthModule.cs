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

using Autofac;

using AzerothSharp.Auth.Network;
using AzerothSharp.Shared;

namespace AzerothSharp.AuthServer;

public class AuthModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Router>().AsSelf().SingleInstance();

        builder.RegisterType<RealmList>().AsSelf().SingleInstance();

        builder.RegisterType<AUTH_LOGON_CHALLENGE_C_Reader>().AsSelf().SingleInstance();
        builder.RegisterType<AUTH_LOGON_CHALLENGE_S_Writer>().AsSelf().SingleInstance();
        builder.RegisterType<AUTH_LOGON_CHALLENGE_Handler>().AsSelf().SingleInstance();

        builder.RegisterType<AUTH_LOGON_PROOF_C_Reader>().AsSelf().SingleInstance();
        builder.RegisterType<AUTH_LOGON_PROOF_S_Writer>().AsSelf().SingleInstance();
        builder.RegisterType<AUTH_LOGON_PROOF_Handler>().AsSelf().SingleInstance();

        builder.RegisterType<AUTH_REALMLIST_Handler>().AsSelf().SingleInstance();
        builder.RegisterType<AUTH_REALMLIST_C_Reader>().AsSelf().SingleInstance();
        builder.RegisterType<AUTH_REALMLIST_S_Writer>().AsSelf().SingleInstance();

        builder.RegisterType<CMD_XFER_RESUME_Handler>().AsSelf().SingleInstance();
        builder.RegisterType<CMD_XFER_RESUME_Reader>().AsSelf().SingleInstance();
        
        builder.RegisterType<Startup>().AsSelf().SingleInstance();
    }
}
