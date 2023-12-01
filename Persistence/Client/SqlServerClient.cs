using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Persistence.Client
{
    public class SqlServerClient
    {
        private readonly string _connectionString;

        public SqlServerClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T> RetrieveSingleRowAsync<T>(string toExecute, DynamicParameters parameters, int? timeout,
            CommandType commandType = CommandType.Text)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<T>(toExecute, parameters, commandTimeout: timeout,
                commandType: commandType);
        }

        public async Task<IEnumerable<T>> RetrieveMultipleRowsAsync<T>(string toExecute,
            DynamicParameters parameters, int? timeout, CommandType commandType = CommandType.Text)
        {
            await using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(toExecute, parameters, commandTimeout: timeout,
                commandType: commandType);
        }

        public async Task<int> ExecuteAsync(string toExecute, DynamicParameters parameters, int? timeout,
        CommandType commandType = CommandType.Text)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var affectedRows = await connection.ExecuteAsync(toExecute,
                parameters,
                commandTimeout: timeout,
                commandType: commandType);

            return affectedRows;
        }
    }
}
