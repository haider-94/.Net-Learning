using Microsoft.Data.SqlClient;
using System.Data;
using QuickPortals.Models;

namespace QuickPortals.Data
{
    public class SqlDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> GetFirstSetupMessageAsync()
        {
            const string sql = "SELECT TOP 1 Message FROM SetupChecks ORDER BY Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(sql, connection);
            var result = await command.ExecuteScalarAsync();
            return result?.ToString() ?? "No data found";
        }

        public async Task<List<AuditLog>> GetAuditLogsAsync()
        {
            var logs = new List<AuditLog>();
            const string sql = "SELECT Id, Action, UserName, Timestamp FROM AuditLogs ORDER BY Id";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                logs.Add(new AuditLog
                {
                    Id = reader.GetInt32(0),
                    Action = reader.GetString(1),
                    UserName = reader.GetString(2),
                    Timestamp = reader.GetDateTime(3)
                });
            }

            return logs;
        }
    }
}
