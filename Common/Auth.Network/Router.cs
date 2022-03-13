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

using AzerothSharp.Common;

namespace AzerothSharp.Auth.Network;

public class Router
{
    private readonly Dictionary<AuthCMD, IPacketHandler> handlers;

    public Router(
            AUTH_LOGON_CHALLENGE_Handler LOGON_CHALLENGE_Handler,
            AUTH_LOGON_PROOF_Handler LOGON_PROOF_Handler,
            AUTH_REALMLIST_Handler REALMLIST_Handler,
            CMD_XFER_RESUME_Handler XFER_RESUME_Handler)
    {
        handlers = new Dictionary<AuthCMD, IPacketHandler>
        {
            [AuthCMD.CMD_AUTH_LOGON_CHALLENGE] = LOGON_CHALLENGE_Handler,
            [AuthCMD.CMD_AUTH_RECONNECT_CHALLENGE] = LOGON_CHALLENGE_Handler,
            [AuthCMD.CMD_AUTH_LOGON_PROOF] = LOGON_PROOF_Handler,
            [AuthCMD.CMD_AUTH_REALMLIST] = REALMLIST_Handler,
            [AuthCMD.CMD_XFER_RESUME] = XFER_RESUME_Handler
        };
    }

    public IPacketHandler? GetPacketHandler(byte opcode)
    {
        AuthCMD authCMD = (AuthCMD)opcode;

        return handlers.ContainsKey(authCMD) ? handlers[authCMD] : null;
    }
}
