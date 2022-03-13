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

using System.Runtime.CompilerServices;

namespace AzerothSharp.Storage;

/// <summary>
/// 
/// </summary>
public interface IStorage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queriesTarget"></param>
    /// <param name="conenctionString"></param>
    public Task ConnectAsync(object queriesTarget, string conenctionString);

    /// <summary>
    /// 
    /// </summary>
    public ValueTask DisposeAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="callerMemberName"></param>
    /// <returns></returns>
    public Task<T> QuerySingleAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="callerMemberName"></param>
    /// <returns></returns>
    public Task<T> QuerySingleOrDefaultAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="callerMemberName"></param>
    /// <returns></returns>
    public Task<T> QueryFirstOrDefaultAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="callerMemberName"></param>
    /// <returns></returns>
    public Task<List<T>> QueryListAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="callerMemberName"></param>
    public Task QueryAsync(object parameters, [CallerMemberName] string? callerMemberName = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="callerMemberName"></param>
    /// <returns></returns>
    public Task<int> ExecuteAsync(object parameters, [CallerMemberName] string? callerMemberName = null);
}
