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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using Dapper;
using MySql.Data.MySqlClient;

namespace AzerothSharp.Storage.MySql;

public class MySqlStorage : IAsyncDisposable, IStorage
{
    private MySqlConnection? m_connection;
    private Dictionary<string, string>? m_queries;

    public async Task ConnectAsync(object queriesTarget, string conenctionString)
    {
        if (m_connection != null)
        {
            throw new Exception("MySql connection has already been opened");
        }

        m_connection = new MySqlConnection(conenctionString);
        var conenctionTask = m_connection.OpenAsync();
        m_queries = GetEmbeddedQueries(queriesTarget);

        await conenctionTask;
    }

    public async ValueTask DisposeAsync()
    {
        if (m_connection != null)
        {
            await m_connection.DisposeAsync();
        }
    }

    private static Dictionary<string, string> GetEmbeddedQueries(object executor)
    {
        var type = executor.GetType();
        var assembly = type.Assembly;
        var queriesCatalog = $"{type.Namespace}.Queries";

        Dictionary<string, string> resources =
            assembly.GetManifestResourceNames()
                        .Where(x => x.StartsWith(queriesCatalog))
                        .ToDictionary(
                            x => GetEmbeddedSqlResourceName(queriesCatalog, x),
                            x => GetEmbeddedSqlResourcebody(assembly, x));

        return resources;
    }

    private static string GetEmbeddedSqlResourcebody(Assembly assembly, string resource)
    {
        using var stream = assembly.GetManifestResourceStream(resource);

        if (stream != null)
        {
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }

        throw new Exception("no embedded sql resource.");
    }

    private string GetQuery(string? query)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        if (m_queries == null)
        {
            throw new Exception("m_queries is empty.");
        }

        return m_queries.ContainsKey(query) ? m_queries[query] : throw new Exception($"Unknown sql query '{query}'");
    }

    private static string GetEmbeddedSqlResourceName(string queriesCatalog, string resource)
    {
        return Regex.Split(resource, $"{queriesCatalog}.(.*).sql")[1];
    }

    public async Task<T> QuerySingleAsync<T>(
                                object parameters,
                                [CallerMemberName] string? callerMemberName = null)
    {
        return await m_connection.QuerySingleAsync<T>(GetQuery(callerMemberName), parameters);
    }

    public async Task<T> QuerySingleOrDefaultAsync<T>(
                                object parameters,
                                [CallerMemberName] string? callerMemberName = null)
    {
        return await m_connection.QuerySingleOrDefaultAsync<T>(GetQuery(callerMemberName), parameters);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(
                                object parameters,
                                [CallerMemberName] string? callerMemberName = null)
    {
        return await m_connection.QueryFirstOrDefaultAsync<T>(GetQuery(callerMemberName), parameters);
    }

    public async Task<List<T>> QueryListAsync<T>(
                                    object parameters,
                                    [CallerMemberName] string? callerMemberName = null)
    {
        var result = await m_connection.QueryAsync<T>(GetQuery(callerMemberName), parameters);

        return result.ToList();
    }

    public async Task QueryAsync(
                           object parameters,
                           [CallerMemberName] string? callerMemberName = null)
    {
        await m_connection.QueryAsync(GetQuery(callerMemberName), parameters);
    }

    public async Task<int> ExecuteAsync(
                                object parameters,
                                [CallerMemberName] string? callerMemberName = null)
    {
        var result = await m_connection.ExecuteAsync(GetQuery(callerMemberName), parameters);

        return result;
    }
}
