using CycleLog.ApiClient.Interfaces;
using CycleLog.DAL.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CycleLog.MVC.Controllers
{
    public class TrainingSessionController : Controller
    {
        private readonly ILogger<TrainingSessionController> _logger;
        private readonly ITrainingSessionApiClient _trainingSessionApiClient;

        public TrainingSessionController(ILogger<TrainingSessionController> logger, ITrainingSessionApiClient trainingSessionApiClient)
        {
            _logger = logger;
            _trainingSessionApiClient = trainingSessionApiClient;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TrainingSessionDTO trainingSession)
        {
            throw new NotImplementedException();
        }
    }
}
