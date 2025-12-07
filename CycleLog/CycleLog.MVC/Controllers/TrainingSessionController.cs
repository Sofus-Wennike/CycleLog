using CycleLog.ApiClient.Interfaces;
using CycleLog.DAL.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

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
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return View(await _trainingSessionApiClient.GetTrainingSessionsByUserIdAsync(userId, accessToken));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] TrainingSessionDTO trainingSession)
        {
            try
            {
                //TODO: Denne metode er ikke færdig!
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                int newId = await _trainingSessionApiClient.CreateTrainingSessionAsync(trainingSession, accessToken);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
