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

        public async Task<IEnumerable<TrainingSession>> GetLeaderboardAsync()
        {
            string sql = @"SELECT u.UserId, u.Username, SUM(ts.DistanceKm) AS DistanceKm FROM TrainingSessions ts JOIN Users u ON ts.UserId = u.UserId GROUP BY u.UserId, u.Username ORDER BY DistanceKm DESC LIMIT 10;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                IEnumerable<TrainingSession> trainingSessions = await connection.QueryAsync<TrainingSession>(sql);

                return trainingSessions;
            }
        }

        public async Task<int> CreateTrainingSessionAsync(TrainingSession trainingSession)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        string updateOrInsertUserSql = @"
                            INSERT INTO Users (UserId, Username) 
                            VALUES (@UserId, @Username) 
                            ON CONFLICT (UserId) DO UPDATE
                            SET Username = EXCLUDED.Username;";

                        await connection.ExecuteAsync(updateOrInsertUserSql,
                            new
                            {
                                UserId = trainingSession.UserId,
                                Username = trainingSession.Username
                            }, transaction);

                        string insertTrainingSessionSql = @"
                            INSERT INTO TrainingSessions (UserId, DistanceKm) 
                            VALUES (@UserId, @DistanceKm) 
                            RETURNING Id;";

                        int newId = await connection.ExecuteScalarAsync<int>(insertTrainingSessionSql,
                            new
                            {
                                UserId = trainingSession.UserId,
                                DistanceKm = trainingSession.DistanceKm
                            }, transaction);

                        await transaction.CommitAsync();

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
            string sql = @"SELECT u.UserId, u.Username, ts.DistanceKm FROM TrainingSessions ts JOIN Users u ON ts.UserId = u.UserId WHERE u.UserId = @UserId;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

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
