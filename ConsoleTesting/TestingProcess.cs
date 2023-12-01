using Application.Services;
using Domain.Entities;
using Serilog;

namespace ConsoleTesting
{
    public class TestingProcess
    {
        private readonly TestingSqlService _sqlTestingService;
        private readonly TestingQueueService _queueTestingService;

        public TestingProcess(TestingSqlService sqlTestingService, TestingQueueService queueTestingService)
        {
            _sqlTestingService = sqlTestingService;
            _queueTestingService = queueTestingService; 
        }

        public async Task TestingInit()
        {
            try
            {
                bool salir = false;

                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("Hola Carlos, Entrando en el programa de pruebas...");
                    Console.WriteLine("Opción 1 - Ingresa datos para la prueba");
                    Console.WriteLine("Opción 2 - Muestra los datos ingresados en la db");
                    Console.WriteLine("Opción 3 - Veamos las colas");
                    Console.WriteLine("Opción 4 - Desencolar");
                    Console.WriteLine("Opción 5 - Salir");
                    Console.WriteLine("Elige una de las opciones");
                    int opcion = Convert.ToInt32(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            Console.WriteLine("Digita los datos....");
                            Console.WriteLine("ingresa el Dni: ");
                            var id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("ingresa el nombre a guardar: ");
                            var name = Console.ReadLine();
                            Console.WriteLine("Guardando...");
                            
                            var solicitud = new QueueObject()
                            {
                                Dni = id,
                                Name = name,
                                Status = true
                            };

                            solicitud.Queue = await _queueTestingService.EnQueueObject(solicitud);
                            await _sqlTestingService.InsertDataQueue(solicitud);
                            Console.WriteLine("guardado realizado...");
                            Console.ReadLine();
                            break;

                        case 2:
                            Console.WriteLine("descargando los datos desde Sql....");

                            var data = await _sqlTestingService.SelectAllDataQueue();

                            foreach (var item in data)
                            {
                                Console.WriteLine("Cadula: {0}, Nombre: {1}", item.Dni, item.Name);
                            }
                            Console.WriteLine("Datos de Sql Descargando....");
                            Console.ReadLine();
                            break;

                        case 3:
                            Console.WriteLine("descargando los datos de las colas....");

                            var result = await _queueTestingService.SearchObjectQueues();

                            foreach (var item in result)
                            {
                                var count = 1;
                                Console.WriteLine("Objeto {0} - Cadula: {1}, Nombre: {2}", count, item.Dni, item.Name);
                                count++;
                            }

                            Console.WriteLine("Datos de las colas Descargando....");
                            Console.ReadLine();
                            break;

                        case 4:
                            Console.WriteLine("Digita el nro de la cola que quieres desencolar....");
                            var queue = Convert.ToInt16(Console.ReadLine());
                            Console.WriteLine("Desencolando....");
                            var objectQueue = await _queueTestingService.UnqueueObject(queue);

                            if(objectQueue.Dni == 0)
                            {
                                Console.WriteLine("las colas estan vacias, no se obtuvo informacion.");
                                Console.ReadLine();
                                break;
                            }

                            await _sqlTestingService.UpdateStatusUnqueue(objectQueue.Dni);

                            Console.WriteLine("Objeto Desencolado");
                            Console.WriteLine("Cadula: {0}, Nombre: {1}, Cola: {2}", objectQueue.Dni, objectQueue.Name, objectQueue.Queue);
                            Console.ReadLine();
                            break;

                        case 5:
                            Console.WriteLine("Has elegido salir de la aplicación");
                            salir = true;
                            break;

                        default:
                            Console.WriteLine("Elige una opcion entre 1 y 5");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $" - (OnError) Error: {e?.Message}  Type: {e?.GetType()}");
            }
            finally
            {
                Console.WriteLine("Saliendo del modo de prueba de programa de Carlos, hasta luego...");
            }
        }
    }
}
