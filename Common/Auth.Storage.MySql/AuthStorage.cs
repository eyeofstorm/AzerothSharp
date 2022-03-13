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

using AzerothSharp.Storage;

namespace AzerothSharp.Auth.Storage.MySql;

/// <summary>
/// 
/// </summary>
public class AuthStorage : IAuthStorage
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IStorage m_storage;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mySqlStorage"></param>
    public AuthStorage(IStorage storage)
    {
        m_storage = storage;
    }

    public Task ConnectAsync(object queriesTarget, string conenctionString)
    {
        return ((IStorage)m_storage).ConnectAsync(queriesTarget, conenctionString);
    }

    public ValueTask DisposeAsync()
    {
        return ((IStorage)m_storage).DisposeAsync();
    }

    public Task<T> QuerySingleAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null)
    {
        return ((IStorage)m_storage).QuerySingleAsync<T>(parameters, callerMemberName);
    }

    public Task<T> QuerySingleOrDefaultAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null)
    {
        return ((IStorage)m_storage).QuerySingleOrDefaultAsync<T>(parameters, callerMemberName);
    }

    public Task<T> QueryFirstOrDefaultAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null)
    {
        return ((IStorage)m_storage).QueryFirstOrDefaultAsync<T>(parameters, callerMemberName);
    }

    public Task<List<T>> QueryListAsync<T>(object parameters, [CallerMemberName] string? callerMemberName = null)
    {
        return ((IStorage)m_storage).QueryListAsync<T>(parameters, callerMemberName);
    }

    public Task QueryAsync(object parameters, [CallerMemberName] string? callerMemberName = null)
    {
        return ((IStorage)m_storage).QueryAsync(parameters, callerMemberName);
    }

    public Task<int> ExecuteAsync(object parameters, [CallerMemberName] string? callerMemberName = null)
    {
        return ((IStorage)m_storage).ExecuteAsync(parameters, callerMemberName);
    }

    // =========================================================================

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account_id"></param>
    /// <returns></returns>
    public async Task<List<RealmCharacterCountEntity>> GetRealmCharacterCountAsync(int account_id)
    {
        return await m_storage.QueryListAsync<RealmCharacterCountEntity>(new { accountID = account_id });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<BuildInfoEntity>> GetAllBuildInfoAsync()
    {
        return await m_storage.QueryListAsync<BuildInfoEntity>(new { });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<RealmEntity>> GetRealmListAsync()
    {
        return await m_storage.QueryListAsync<RealmEntity>(new { });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public async Task<AccountInfoEntity> GetAccountInfoAsync(string userName, string ipAddress)
    {
        return await QueryFirstOrDefaultAsync<AccountInfoEntity>(
                            new
                            {
                                username = userName,
                                ip = ipAddress
                            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="session_key"></param>
    /// <param name="last_iP"></param>
    /// <param name="operating_system"></param>
    /// <param name="user_name"></param>
    /// <returns></returns>
    public async Task<int> UpdateLogonProofAsync(
                            byte[] session_key,
                            string last_iP,
                            string operating_system,
                            string user_name)
    {
        return await ExecuteAsync(
                            new
                            {
                                sessionKey      = session_key,
                                lastIp          = last_iP,
                                operatingSystem = operating_system, 
                                userName        = user_name
                            });
    }
}
