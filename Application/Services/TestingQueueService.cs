using Domain.Entities;
using System.Collections.Concurrent;

namespace Application.Services
{
    public class TestingQueueService
    {
        public TestingQueueService() { }

        private static ConcurrentQueue<QueueObject> Queue1 = new ConcurrentQueue<QueueObject>();
        private static ConcurrentQueue<QueueObject> Queue2 = new ConcurrentQueue<QueueObject>();

        public async Task<int> EnQueueObject(QueueObject solicitud) 
        {
            var queue = 0;
            if (Queue1.Count()<=Queue2.Count() && Queue1.Count() < 4)
            {
                Queue1.Enqueue(solicitud);
                queue = 1;
            }                  
            else if(Queue2.Count() <= Queue1.Count() && Queue2.Count() < 4)
            {
                Queue2.Enqueue(solicitud);
                queue = 2;
            }
            return queue;      
        }
        public async Task<string> CountElementsQueues()
        {
            var queue1 = Queue1.Count();
            var queue2 = Queue2.Count();

            return null;
        }
        public async Task<IEnumerable<QueueObject>> SearchObjectQueues()
        {
            var ListQueueObject = new List<QueueObject>();

            if(Queue1.Count() > 0)
            {
                foreach (var solicitud in Queue1)
                {
                    ListQueueObject.Add(solicitud);
                }
            }

            if(Queue2.Count() > 0)
            {
                foreach (var solicitud in Queue2)
                {
                    ListQueueObject.Add(solicitud);
                }
            }

            return ListQueueObject;
        }
        public async Task<QueueObject> UnqueueObject(int queue)
        {
            var objectQueue = new QueueObject();

            if (queue == 1)
            {
                if (Queue1.Count() > 0)
                {
                    Queue1.TryDequeue(out objectQueue);
                }
                    
            }
            else if (queue == 2)
            {
                if (Queue2.Count() > 0) 
                {   
                    Queue2.TryDequeue(out objectQueue);
                }
            }
            return objectQueue;
        }
    }
}
