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

namespace AzerothSharp.Auth;

public struct AccountInfoEntity
{
    public int      id;
    public string   username;
    public int      lock_ip;
    public string   lock_country;
    public string   last_ip;
    public int      failed_logins;
    public string   col6;
    public string   col7;
    public string   col8;
    public string   col9;
    public int      gmlevel;
    public string   totp_secret;
    public int      expansion;
    public byte[]   salt;
    public byte[]   verifier;
}
