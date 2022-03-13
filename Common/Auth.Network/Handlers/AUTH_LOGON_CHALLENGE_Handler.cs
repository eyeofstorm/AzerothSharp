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

using System.Text;
using System.Threading.Channels;

using AzerothSharp.Auth.Storage;
using AzerothSharp.Common;
using AzerothSharp.Cryptography;
using AzerothSharp.Logging;

namespace AzerothSharp.Auth.Network;

public class AUTH_LOGON_CHALLENGE_Handler : IPacketHandler
{
    private readonly ILogger m_logger;
    private readonly GlobalConstants m_globalConstants;
    private readonly IAuthStorage m_authStorage;

    private readonly AUTH_LOGON_CHALLENGE_C_Reader m_requestReader;
    private readonly AUTH_LOGON_CHALLENGE_S_Writer m_responseWriter;

    public AUTH_LOGON_CHALLENGE_Handler(
                ILogger logger,
                GlobalConstants globalConstants,
                IAuthStorage authStorage,
                AUTH_LOGON_CHALLENGE_C_Reader reader,
                AUTH_LOGON_CHALLENGE_S_Writer writer)
    {
        m_logger = logger;
        m_globalConstants = globalConstants;
        m_authStorage = authStorage;

        m_requestReader = reader;
        m_responseWriter = writer;
    }

    public async Task HandleAsync(
                        ChannelReader<byte> reader,
                        ChannelWriter<byte> writer,
                        AuthSession authSession)
    {
        AUTH_LOGON_CHALLENGE_C request = await m_requestReader.ReadAsync(reader);

        authSession.OS = new String(Encoding.UTF8.GetString(request.OS).Reverse().SkipWhile(v => v == 0).ToArray());
        authSession.AccountName = request.AccountName;
        authSession.ClientBuild = request.ClientBuild;
        authSession.Expversion = AuthHelper.IsPostBCAcceptedClientBuild(request.ClientBuild) ? (UInt16)ExpansionFlags.POST_BC_EXP_FLAG : (AuthHelper.IsPreBCAcceptedClientBuild(request.ClientBuild) ? (UInt16)ExpansionFlags.PRE_BC_EXP_FLAG : (UInt16)ExpansionFlags.NO_VALID_EXP_FLAG);

        // Check if our build can join the server
        if (request.ClientBuild == GlobalConstants.Required_Build_3_35_A)
        {
            AccountInfoEntity accountInfo;

            try
            {
                accountInfo =
                    await m_authStorage.GetAccountInfoAsync(
                                                authSession.AccountName,
                                                authSession.RemoteEnpoint.Address.ToString());

                authSession.AccountInfo = accountInfo;

                await HandleLoginOkStateAsync(request, writer, authSession).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                m_logger.Error(e);

                await m_responseWriter.WriteErrorAsync(writer, AuthResult.WOW_FAIL_UNKNOWN_ACCOUNT);
            }
        }
        else
        {
            m_logger.Debug($"WRONG_VERSION {request.ClientBuild}");

            await m_responseWriter.WriteErrorAsync(writer, AuthResult.WOW_FAIL_VERSION_INVALID);
        }
    }

    private async Task HandleLoginOkStateAsync(
                            AUTH_LOGON_CHALLENGE_C request,
                            ChannelWriter<byte> writer,
                            AuthSession authSession)
    {
        // calc SPR6
        authSession.Srp6 = new SrpServerAuth(
                                authSession.AccountInfo.username,
                                authSession.AccountInfo.salt,
                                authSession.AccountInfo.verifier);

        // make response
        AUTH_LOGON_CHALLENGE_S resposne = new(authSession.Srp6.ServerPublicKey,
                                              SrpServerAuth.Generator,
                                              SrpServerAuth.SafePrime,
                                              authSession.Srp6.Salt);

        await m_responseWriter.WriteAsync(writer, resposne);
    }
}
