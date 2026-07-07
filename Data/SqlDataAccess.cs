using Microsoft.Data.SqlClient;
using System.Data;

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
    }
}
