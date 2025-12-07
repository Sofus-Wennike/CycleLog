using CycleLog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.DAL.Interfaces
{
    public interface ITrainingSessionDAO
    {
        public Task<IEnumerable<TrainingSession>> GetTrainingSessionsByUserIdAsync(string userId);

        public Task<int> CreateTrainingSessionAsync(TrainingSession trainingSession);
    }
}
