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

namespace AzerothSharp.Common;

public enum AuthCMD : byte
{
    CMD_AUTH_LOGON_CHALLENGE = 0x0,
    CMD_AUTH_LOGON_PROOF = 0x1,
    CMD_AUTH_RECONNECT_CHALLENGE = 0x2,
    CMD_AUTH_RECONNECT_PROOF = 0x3,
    CMD_AUTH_REALMLIST = 0x10,
    CMD_XFER_INITIATE = 0x30,
    CMD_XFER_DATA = 0x31,
    CMD_XFER_ACCEPT = 0x32,
    CMD_XFER_RESUME = 0x33,
    CMD_XFER_CANCEL = 0x34
}
