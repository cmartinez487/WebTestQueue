using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestingSqlService _sqlTestingService;
        private readonly TestingQueueService _queueTestingService;
        private static bool _init = true;
        public HomeController(TestingSqlService sqlTestingService, TestingQueueService queueTestingService)
        {
            _sqlTestingService = sqlTestingService;
            _queueTestingService = queueTestingService;            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = new List<QueueViewModel>();

            var model = await _sqlTestingService.SelectAllDataQueue();

            foreach (var item in model)
            {
                if (_init)
                {
                    await _queueTestingService.EnQueueObject(item);
                }

                result.Add( new QueueViewModel 
                { 
                    Dni = item.Dni,
                    Name = item.Name,
                    Status = item.Status,
                    Queue = item.Queue,
                });
            }
            _init = false;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int queue)
        {
            var result = new List<QueueViewModel>();

            var sol =  await _queueTestingService.UnqueueObject(queue);
            await _sqlTestingService.UpdateStatusUnqueue(sol.Dni);
            var model = await _sqlTestingService.SelectAllDataQueue();

            foreach (var item in model)
            {
                result.Add(new QueueViewModel
                {
                    Dni = item.Dni,
                    Name = item.Name,
                    Status = item.Status,
                    Queue = item.Queue,
                });
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel register)
        {
            var solicitud = new QueueObject()
            {
                Dni = register.Dni,
                Name = register.Name,
                Status = true
            };

            solicitud.Queue = await _queueTestingService.EnQueueObject(solicitud);
            await _sqlTestingService.InsertDataQueue(solicitud);

            if(solicitud.Queue == 0)
            {
                ViewData["mensaje"] = "Tu solicitu no ser guardada, las colas estan a su maxima capacidad.... ";
            }
            else
            {
                ViewData["mensaje"] = "Tu solicitu se guardo con exito";
            }
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
