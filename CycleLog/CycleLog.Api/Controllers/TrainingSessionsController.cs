using CycleLog.DAL.DTO;
using CycleLog.DAL.Interfaces;
using CycleLog.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<ActionResult> GetTrainingSessionsByUserIdAsync(string userId)
        {
            if (!userId.Equals(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return Unauthorized();
            }

            try
            {
                return Ok(await _trainingSessionDAO.GetTrainingSessionsByUserIdAsync(userId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); //TODO: ex.Message skal måske ikke sendes med!
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> CreateTrainingSessionAsync([FromBody] TrainingSessionDTO dto)
        {
            try
            {
                //TODO: Lav en rigtig mapper!
                TrainingSession trainingSession = new TrainingSession
                {
                    Id = dto.Id,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    DistanceKm = dto.DistanceKm
                };

                int newId = await _trainingSessionDAO.CreateTrainingSessionAsync(trainingSession);

                return Ok(newId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //TODO: ex.Message skal måske ikke sendes med!
            }
        }
    }
}
