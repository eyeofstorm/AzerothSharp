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

using AzerothSharp.Network.Tcp;

namespace AzerothSharp.Auth.Network;

public class CMD_XFER_RESUME_Reader : IPacketReader<CMD_XFER_RESUME_C>
{
    public async ValueTask<CMD_XFER_RESUME_C> ReadAsync(ChannelReader<byte> reader)
    {
        var unk = await reader.ReadArrayAsync(8);

        return new CMD_XFER_RESUME_C(unk);
    }
}
