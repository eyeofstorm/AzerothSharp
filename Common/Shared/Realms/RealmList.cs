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

using System.Net;
using System.Text;

using AzerothSharp.Auth;
using AzerothSharp.Auth.Storage;
using AzerothSharp.Common;
using AzerothSharp.Logging;

namespace AzerothSharp.Shared;

/// <summary>
/// 
/// </summary>
public class RealmList
{
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger m_logger;

    /// <summary>
    /// 
    /// </summary>
    private readonly IAuthStorage m_authStorage;

    /// <summary>
    /// 
    /// </summary>
    private readonly RealmMap m_realms;

    /// <summary>
    /// 
    /// </summary>
    private readonly List<RealmBuildInfo> m_builds;

    /// <summary>
    /// 
    /// </summary>
    private PeriodicTimer? m_updateTimer;

	/// <summary>
    /// 
    /// </summary>
	public RealmList(ILogger logger, IAuthStorage authStorage)
    {
        m_logger = logger;
        m_authStorage = authStorage;

        m_realms = new();
        m_builds = new();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public RealmMap GetRealms()
    {
        return m_realms;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateInterval"></param>
    public async Task InitializeAsync(UInt32 updateInterval)
    {
        m_updateTimer = new PeriodicTimer(TimeSpan.FromSeconds(updateInterval));

        await LoadBuildInfoAsync();

        using var t = Task.Factory.StartNew(async () =>
        {
            while (await m_updateTimer.WaitForNextTickAsync())
            {
                await UpdateRealms();
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="build"></param>
    /// <returns></returns>
    public RealmBuildInfo? GetBuildInfo(ushort build)
    {
        return m_builds.FirstOrDefault(x => x.Build == build);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Close()
    {
        m_updateTimer?.Dispose();
    }

    /// <summary>
    /// 
    /// </summary>
    private async Task LoadBuildInfoAsync()
    {
        List<BuildInfoEntity> buildInfoList = await m_authStorage.GetAllBuildInfoAsync();

        foreach (var buildInfo in buildInfoList)
        {
            RealmBuildInfo realmBuildInfo = new();

            realmBuildInfo.Build = buildInfo.build;
            realmBuildInfo.MajorVersion = buildInfo.majorVersion;
            realmBuildInfo.MinorVersion = buildInfo.minorVersion;
            realmBuildInfo.BugfixVersion = buildInfo.bugfixVersion;

            byte[] hotfixVersion = Encoding.ASCII.GetBytes(buildInfo.hotfixVersion);

            if (realmBuildInfo.HotfixVersion.Length < hotfixVersion.Length)
            {
                Array.Copy(hotfixVersion, 0, realmBuildInfo.HotfixVersion, 0, hotfixVersion.Length);
            }
            else
            {
                Array.Fill<byte>(realmBuildInfo.HotfixVersion, 0x00);
            }

            realmBuildInfo.WindowsHash = StringUtils.HexStringToByteArray(buildInfo.winChecksumSeed);
            realmBuildInfo.MacHash = StringUtils.HexStringToByteArray(buildInfo.macChecksumSeed);

            m_builds.Add(realmBuildInfo);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private async Task UpdateRealms()
    {

        m_logger.Debug("Updating Realm List...");

        Dictionary<RealmHandle, String> existingRealms = new();

        foreach (var item in m_realms)
        {
            existingRealms[item.Key] = item.Value.Name;
        }

        m_realms.Clear();

        List<RealmEntity> realmList = await m_authStorage.GetRealmListAsync();

        // Circle through results and add them to the realm map
        if (realmList.Count > 0)
        {
            foreach (RealmEntity realmEntity in realmList)
            {
                byte icon = realmEntity.icon;

                if (icon == (byte)RealmType.REALM_TYPE_FFA_PVP)
                {
                    icon = (byte)RealmType.REALM_TYPE_PVP;
                }

                if (icon >= (byte)RealmType.MAX_CLIENT_REALM_TYPE)
                {
                    icon = (byte)RealmType.REALM_TYPE_NORMAL;
                }

                AccountTypes secLevel =
                    realmEntity.allowedSecurityLevel <= (byte)AccountTypes.SEC_ADMINISTRATOR ?
                                (AccountTypes)realmEntity.allowedSecurityLevel :
                                AccountTypes.SEC_ADMINISTRATOR;

                Realm realm = new()
                {
                    Id = new RealmHandle(realmEntity.id),
                    Build = realmEntity.gamebuild,
                    ExternalAddress = IPAddress.Parse(realmEntity.address),
                    LocalAddress = IPAddress.Parse(realmEntity.localAddress),
                    LocalSubnetMask = IPAddress.Parse(realmEntity.localSubnetMask),
                    Port = realmEntity.port,
                    Name = realmEntity.name,
                    Type = icon,
                    Flags = (RealmFlags)realmEntity.flag,
                    Timezone = realmEntity.timezone,
                    AllowedSecurityLevel = secLevel,
                    PopulationLevel = realmEntity.population
                };

                m_realms[realm.Id] = realm;
            }
        }
    }
}
