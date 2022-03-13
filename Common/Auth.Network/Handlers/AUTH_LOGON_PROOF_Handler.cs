/*
 *This file is part of the AzerothCore Project. See AUTHORS file for Copyright information
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
using AzerothSharp.Cryptography;
using AzerothSharp.Logging;

namespace AzerothSharp.Auth.Network;

public class AUTH_LOGON_PROOF_Handler : IPacketHandler
{
    private readonly ILogger m_logger;
    private readonly IAuthStorage m_accountStorage;

    private readonly AUTH_LOGON_PROOF_C_Reader m_requestReqder;
    private readonly AUTH_LOGON_PROOF_S_Writer m_responseWriter;

    public AUTH_LOGON_PROOF_Handler(
                ILogger logger,
                IAuthStorage accountStorage,
                AUTH_LOGON_PROOF_C_Reader requestReqder,
                AUTH_LOGON_PROOF_S_Writer responseWriter)
    {
        m_logger = logger;
        m_accountStorage = accountStorage;
        m_requestReqder = requestReqder;
        m_responseWriter = responseWriter;
    }

    public async Task HandleAsync(
                        ChannelReader<byte> reader,
                        ChannelWriter<byte> writer,
                        AuthSession clientInfo)
    {
        AUTH_LOGON_PROOF_C request = await m_requestReqder.ReadAsync(reader);

        byte[]? sessionKey = clientInfo.Srp6?.VerifyChallengeResponse(
                                                    request.ClientPublicKey,
                                                    request.ClientM1);

        // Check if SRP6 results match (password is correct), else send an error
        if (sessionKey != null)
        {
            try
            {
                // login ok.
                await m_accountStorage.UpdateLogonProofAsync(
                                            sessionKey,
                                            clientInfo.RemoteEnpoint.Address.ToString(),
                                            clientInfo.OS,
                                            clientInfo.AccountName);

                byte[] m2 = SrpServerAuth.ComputeM2(request.ClientPublicKey, request.ClientM1, sessionKey);

                var proof = new AUTH_LOGON_PROOF_S
                {
                    Command = (byte)AuthCMD.CMD_AUTH_LOGON_PROOF,
                    M2 = m2,
                    Error = 0,
                    AccountFlags = 0x00800000,
                    SurveyId = 0,
                    LoginFlags = 0
                };

                await m_responseWriter.WriteAsync(writer, proof);

                m_logger.Debug($"Auth success for user {clientInfo.AccountName}");
            }
            catch (Exception e)
            {
                // database update error.
                m_logger.Error($"database update error for user {clientInfo.AccountName}.");
                m_logger.Error(e);

                await m_responseWriter.WriteErrorAsync(
                                        writer,
                                        AuthResult.WOW_FAIL_DB_BUSY);
            }
        }
        else
        {
            // login error. invalid password.
            m_logger.Error($"Wrong password for user {clientInfo.AccountName}.");

            await m_responseWriter.WriteErrorAsync(
                                        writer,
                                        AuthResult.WOW_FAIL_INCORRECT_PASSWORD);
        }
    }
}
