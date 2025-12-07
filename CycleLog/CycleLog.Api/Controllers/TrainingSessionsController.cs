using CycleLog.DAL.DTO;
using CycleLog.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CycleLog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TrainingSessionsController : ControllerBase
    {
        private readonly ILogger<TrainingSessionsController> _logger;
        private readonly ITrainingSessionDAO _trainingSessionDAO;

        public TrainingSessionsController(ILogger<TrainingSessionsController> logger, ITrainingSessionDAO trainingSessionDAO)
        {
            _logger = logger;
            _trainingSessionDAO = trainingSessionDAO;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetTrainingSessionsByUserId(string userId)
        {
            //TODO: Husk at færdiggøre denne metode!
            return Ok();
        }
    }
}
