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
using System.Xml.Serialization;

namespace AzerothSharp.Auth.Configuration;

[XmlRoot(ElementName = "AuthServer")]
public class AuthServerConfiguration
{
    public string AuthServerEndpoint { get; set; } = "127.0.0.1:3724";
    public string AccountConnectionString { get; set; } = "Server=localhost;Port=3306;User ID=acore;Password=acore;Database=acore_auth;Compress=false;Connection Timeout=1;";
}
