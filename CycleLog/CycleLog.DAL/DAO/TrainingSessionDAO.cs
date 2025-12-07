using CycleLog.DAL.Interfaces;
using CycleLog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.DAL.DAO
{
    public class TrainingSessionDAO : ITrainingSessionDAO
    {
        private readonly string _connectionString;

        public TrainingSessionDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateTrainingSession(TrainingSession trainingSession)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TrainingSession>> GetTrainingSessionsByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
