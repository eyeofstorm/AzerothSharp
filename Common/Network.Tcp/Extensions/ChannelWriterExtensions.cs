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
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AzerothSharp.Network.Tcp;

public static class ChannelWriterExtensions
{
    public static async ValueTask WriteEnumerableAsync(this ChannelWriter<byte> writer, IEnumerable<byte> data)
    {
        foreach (var item in data)
        {
            await writer.WriteAsync(item);
        }
    }

    public static async ValueTask WriteFloatAsync(this ChannelWriter<byte> writer, float data)
    {
        await writer.WriteEnumerableAsync(BitConverter.GetBytes(data));
    }

    public static async ValueTask WriteZeroNCountAsync(this ChannelWriter<byte> writer, int count)
    {
        for (var i = 0; i < count; i++)
        {
            await writer.WriteAsync(0);
        }
    }

    public static async ValueTask WriteArrayAsync(this ChannelWriter<byte> writer, byte[] data, int count)
    {
        for (var i = 0; i < count; i++)
        {
            await writer.WriteAsync(data[i]);
        }
    }
}
