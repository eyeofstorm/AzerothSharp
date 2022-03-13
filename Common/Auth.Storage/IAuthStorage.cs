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

using AzerothSharp.Auth;
using AzerothSharp.Storage;

namespace AzerothSharp.Auth.Storage;

/// <summary>
/// 
/// </summary>
public interface IAuthStorage : IStorage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountName"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public Task<AccountInfoEntity> GetAccountInfoAsync(string accountName, string ipAddress);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sessionKey"></param>
    /// <param name="lastIP"></param>
    /// <param name="os"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public Task<int> UpdateLogonProofAsync(byte[] sessionKey, string lastIP, string os, string userName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account_id"></param>
    /// <returns></returns>
    public Task<List<RealmCharacterCountEntity>> GetRealmCharacterCountAsync(int account_id);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<BuildInfoEntity>> GetAllBuildInfoAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<List<RealmEntity>> GetRealmListAsync();
}
