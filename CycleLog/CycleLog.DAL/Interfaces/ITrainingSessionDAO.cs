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
        public Task<IEnumerable<TrainingSession>> GetTrainingSessionsByUserId(string userId);

        public Task<int> CreateTrainingSession(TrainingSession trainingSession);
    }
}
