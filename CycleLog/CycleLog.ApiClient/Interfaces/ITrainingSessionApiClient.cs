using CycleLog.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.ApiClient.Interfaces
{
    public interface ITrainingSessionApiClient
    {
        public Task<IEnumerable<TrainingSessionDTO>> GetTrainingSessionsByUserIdAsync(string userId, string accessToken);

        public Task<int> CreateTrainingSessionAsync(TrainingSessionDTO trainingSession, string accessToken);
    }
}
