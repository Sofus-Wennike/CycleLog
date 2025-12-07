using CycleLog.DAL.Interfaces;
using CycleLog.DAL.Models;
using Dapper;
using Npgsql;
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

        public async Task<int> CreateTrainingSessionAsync(TrainingSession trainingSession)
        {
            string sql = @"INSERT INTO TrainingSessions (UserId, DistanceKm) VALUES (@UserId, @DistanceKm) RETURNING Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int newId = await connection.ExecuteScalarAsync<int>(sql,
                            new
                            {
                                UserId = trainingSession.UserId,
                                DistanceKm = trainingSession.DistanceKm
                            }, 
                            transaction);

                        transaction.Commit();

                        return newId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception($"Error inserting TrainingSession. Message was {ex.Message}");
                    }
                }
            }
        }

        public async Task<IEnumerable<TrainingSession>> GetTrainingSessionsByUserIdAsync(string userId)
        {
            string sql = @"SELECT * FROM TrainingSessions WHERE UserId = @UserId;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                IEnumerable<TrainingSession> trainingSessions = await connection.QueryAsync<TrainingSession>(sql,
                    new
                    {
                        UserId = userId
                    });

                return trainingSessions;
            }
        }
    }
}
