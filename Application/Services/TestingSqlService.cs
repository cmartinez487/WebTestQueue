using Domain.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class TestingSqlService
    {
        private readonly SqlTestingRepository _sqlTestingRepository;
        public TestingSqlService(SqlTestingRepository sqlTestingRepository) 
        {
            _sqlTestingRepository = sqlTestingRepository;
        }

        public async Task InsertDataQueue(QueueObject solicitud)
        {
            solicitud.Status = solicitud.Queue == 0 ? false : true;
            if(solicitud.Status)
                await _sqlTestingRepository.InsertDataTesting(solicitud);
        }
        public async Task<IEnumerable<QueueObject>> SelectAllDataQueue()
        {
            return await _sqlTestingRepository.selectAllDataTesting();
        }
        public async Task<QueueObject> SelectDataQueue(int id)
        {
            return await _sqlTestingRepository.selectDataTesting(id);
        }
        public async Task UpdateStatusUnqueue(int dni)
        {
            await _sqlTestingRepository.UpdateDataTesting(dni);
        }
    }
}
