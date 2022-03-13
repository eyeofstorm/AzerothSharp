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

using System.Text;
using System.Threading.Channels;

using AzerothSharp.Common;
using AzerothSharp.Network.Tcp;

namespace AzerothSharp.Auth.Network;

public class AUTH_REALMLIST_S_Writer : IPacketWriter<AUTH_REALMLIST_S>
{
    public async ValueTask WriteAsync(ChannelWriter<byte> writer, AUTH_REALMLIST_S packet)
    {
        await writer.WriteAsync((byte)AuthCMD.CMD_AUTH_REALMLIST);

        // TODO: implement this.
    }
}
