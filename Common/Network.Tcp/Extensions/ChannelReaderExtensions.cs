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
using System.Threading.Tasks;

namespace AzerothSharp.Network.Tcp;

public static class ChannelReaderExtensions
{
    public static async ValueTask ReadToArrayAsync(this ChannelReader<byte> reader, byte[] buffer, int offset, int count)
    {
        for (var i = 0; i < count; i++)
        {
            buffer[offset + i] = await reader.ReadAsync();
        }
    }

    public static async ValueTask<byte[]> ReadArrayAsync(this ChannelReader<byte> reader, int count)
    {
        var buffer = new byte[count];

        for (var i = 0; i < count; i++)
        {
            buffer[i] = await reader.ReadAsync();
        }

        return buffer;
    }

    public static async ValueTask ReadVoidAsync(this ChannelReader<byte> reader, int count)
    {
        for (var i = 0; i < count; i++)
        {
            await reader.ReadAsync();
        }
    }
}
