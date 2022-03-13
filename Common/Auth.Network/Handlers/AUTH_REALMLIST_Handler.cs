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

using AzerothSharp.Auth.Storage;
using AzerothSharp.Common;
using AzerothSharp.Shared;

namespace AzerothSharp.Auth.Network;

/// <summary>
/// 
/// </summary>
public class AUTH_REALMLIST_Handler : IPacketHandler
{
    private readonly IAuthStorage m_authStorage;
    private readonly RealmList m_realmList;
    private readonly AUTH_REALMLIST_C_Reader m_requestReader;
    private readonly AUTH_REALMLIST_S_Writer m_responseWriter;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authStorage"></param>
    /// <param name="requestReader"></param>
    /// <param name="responseWriter"></param>
    public AUTH_REALMLIST_Handler(
                IAuthStorage authStorage,
                RealmList realmList,
                AUTH_REALMLIST_C_Reader requestReader,
                AUTH_REALMLIST_S_Writer responseWriter)
    {
        m_authStorage = authStorage;
        m_realmList = realmList;
        m_requestReader = requestReader;
        m_responseWriter = responseWriter;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="writer"></param>
    /// <param name="authSession"></param>
    /// <returns></returns>
    public async Task HandleAsync(
                        ChannelReader<byte> reader,
                        ChannelWriter<byte> writer,
                        AuthSession authSession)
    {
        AUTH_REALMLIST_S response = new();

        List<RealmCharacterCountEntity> numChars =
            await m_authStorage.GetRealmCharacterCountAsync(authSession.AccountInfo.id);

        Dictionary<int, int> characterCounts = new();

        foreach (var item in numChars)
        {
            characterCounts[item.realmid] = item.numchars;
        }

        foreach (var keyValPair in m_realmList.GetRealms())
        {
            Realm realm = keyValPair.Value;

            // don't work with realms which not compatible with the client
            bool okBuild =
                    ((authSession.Expversion & (uint)ExpansionFlags.POST_BC_EXP_FLAG) == (uint)ExpansionFlags.POST_BC_EXP_FLAG && realm.Build == authSession.ClientBuild) ||
                    ((authSession.Expversion & (uint)ExpansionFlags.PRE_BC_EXP_FLAG) == (uint)ExpansionFlags.PRE_BC_EXP_FLAG) && !AuthHelper.IsPreBCAcceptedClientBuild(realm.Build);

            // No SQL injection. id of realm is controlled by the database.
            RealmFlags flag = realm.Flags;

            RealmBuildInfo? buildInfo = m_realmList.GetBuildInfo(realm.Build);

            if (!okBuild)
            {
                if (buildInfo != null)
                {
                    continue;
                }

                // tell the client what build the realm is for
                flag |= RealmFlags.REALM_FLAG_OFFLINE | RealmFlags.REALM_FLAG_SPECIFYBUILD;
            }

            if (buildInfo != null)
            {
                flag &= ~RealmFlags.REALM_FLAG_SPECIFYBUILD;
            }

            // TODO: implement this
        }

        await m_responseWriter.WriteAsync(writer, response);
    }
}
