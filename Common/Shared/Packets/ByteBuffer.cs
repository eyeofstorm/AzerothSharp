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

namespace AzerothSharp.Shared;

/// <summary>
/// 
/// </summary>
public class ByteBuffer
{
	/// <summary>
    /// 
    /// </summary>
	private const int DEFAULT_SIZE = 0x1000;

	/// <summary>
    /// 
    /// </summary>
	private int m_rpos;

	/// <summary>
    /// 
    /// </summary>
	private int m_wpos;

	/// <summary>
    /// 
    /// </summary>
	private List<byte> m_storage;

	/// <summary>
    /// 
    /// </summary>
	public ByteBuffer()
	{
		m_rpos = 0;
		m_wpos = 0;
		m_storage = new List<byte>(DEFAULT_SIZE);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="capacity"></param>
	public ByteBuffer(int capacity)
	{
		m_rpos = 0;
		m_wpos = 0;
        m_storage = new List<byte>(capacity);
	}

	/// <summary>
    /// 
    /// </summary>
	public void Clear()
	{
		m_storage.Clear();
		m_rpos = m_wpos = 0;
	}
}
