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

namespace AzerothSharp.Common;

public static class StringUtils
{
	public static byte[] HexStringToByteArray(String hexString, bool isReverse = false)
    {
        if (hexString.Length == 0 || hexString.Length % 2 != 0)
        {
            throw new ArgumentException("not a valid hex string", nameof(hexString));
        }

        Char[] str = hexString.ToCharArray();
        byte[] retArr = new byte[hexString.Length / 2];

        Int32 init = 0;
        Int32 end = hexString.Length;
        sbyte op = 1;

        if (isReverse)
        {
            init = hexString.Length - 2;
            end = -2;
            op = -1;
        }

        UInt32 j = 0;

        for (Int32 i = init; i != end; i += 2 * op)
        {
            retArr[j++] = Convert.ToByte(new string(new Char[] { str[i], str[i + 1] }), 16);
        }

        return retArr;
    }
}
