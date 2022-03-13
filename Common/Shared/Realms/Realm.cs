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
using System.Net;
using System.Net.Sockets;

using AzerothSharp.Common;

namespace AzerothSharp.Shared;

public struct Realm
{
    public RealmHandle      Id                      { get; set; }
    public UInt16           Build                   { get; set; }
    public IPAddress        ExternalAddress         { get; set; }
    public IPAddress        LocalAddress            { get; set; }
    public IPAddress        LocalSubnetMask         { get; set; }
    public Int16            Port                    { get; set; }
    public String           Name                    { get; set; }
    public Byte             Type                    { get; set; }
    public RealmFlags       Flags                   { get; set; }
    public Byte             Timezone                { get; set; }
    public AccountTypes     AllowedSecurityLevel    { get; set; }
    public float            PopulationLevel         { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientAddr"></param>
    /// <returns></returns>
    public IPEndPoint GetAddressForClient(IPAddress clientAddr)
    {
        IPAddress realmIp;

        // Attempt to send best address for client
        if (IPAddress.IsLoopback(clientAddr))
        {
            // Try guessing if realm is also connected locally
            if (IPAddress.IsLoopback(LocalAddress) || IPAddress.IsLoopback(ExternalAddress))
            {
                realmIp = clientAddr;
            }
            else
            {
                // Assume that user connecting from the machine that bnetserver is located on
                // has all realms available in his local network
                realmIp = LocalAddress;
            }
        }
        else
        {
            if (clientAddr.AddressFamily == AddressFamily.InterNetwork &&
                NetworkUtils.IsInNetwork(LocalAddress, LocalSubnetMask, clientAddr))
            {
                realmIp = LocalAddress;
            }
            else
            {
                realmIp = ExternalAddress;
            }
        }

        // Return external IP
        return new IPEndPoint(realmIp, Port);
    }
}
