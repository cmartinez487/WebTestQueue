using Dapper;
using Domain.Entities;
using Persistence.Client;

namespace Persistence.Repositories
{
    public class SqlTestingRepository
    {
        private SqlServerClient _sqlServerClient;

        public SqlTestingRepository(SqlServerClient sqlServerClient)
        {
            _sqlServerClient = sqlServerClient;
        }

        public async Task InsertDataTesting(QueueObject solicitud)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Dni", solicitud.Dni);
            parameters.Add("@Name", solicitud.Name);
            parameters.Add("@Status", solicitud.Status);
            parameters.Add("@Queue", solicitud.Queue);

            var query = $@"INSERT INTO TestingQueue (dni, name, queue, status) 
                           VALUES (@Dni, @Name, @Queue, @Status)";

            await _sqlServerClient.ExecuteAsync(query, parameters, null);
        }
        public async Task UpdateDataTesting(int Dni)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Dni", Dni);

            var query = $@"UPDATE TestingQueue SET status = 0
                           WHERE DNI = @Dni";

            await _sqlServerClient.ExecuteAsync(query, parameters, null);
        }
        public async Task<IEnumerable<QueueObject>> selectAllDataTesting()
        {
            var query = $@"SELECT dni, name, queue, status 
                           From TestingQueue
                           WHERE status = 1";
            return await _sqlServerClient.RetrieveMultipleRowsAsync<QueueObject>(query, null, null);
        }
        public async Task<QueueObject> selectDataTesting(int Dni)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Dni", Dni); ;

            var query = $@"SELECT dni, name, status 
                           FROM TestingQueue
                           WHERE dni = @Dni";
            return await _sqlServerClient.RetrieveSingleRowAsync<QueueObject>(query, parameters, null);
        }
    }
}