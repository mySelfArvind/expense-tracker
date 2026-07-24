using Dapper;
using Npgsql;
using System.Data;

namespace ExpenseTracker.Helpers
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ExpenseTrackerDefault")!;
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
