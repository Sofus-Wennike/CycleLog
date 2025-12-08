using CycleLog.ApiClient.Interfaces;
using CycleLog.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CycleLog.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrainingSessionApiClient _trainingSessionApiClient;

        public HomeController(ILogger<HomeController> logger, ITrainingSessionApiClient trainingSessionApiClient)
        {
            _logger = logger;
            _trainingSessionApiClient = trainingSessionApiClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _trainingSessionApiClient.GetLeaderboardAsync());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Logout()
        {
            var redirectUri = Url.Action("Index", "Home", null, Request.Scheme);

            return SignOut(new AuthenticationProperties
            {
                RedirectUri = redirectUri
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
