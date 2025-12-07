using CycleLog.ApiClient.Interfaces;
using CycleLog.DAL.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.ApiClient.ApiClients
{
    public class TrainingSessionApiClient : ITrainingSessionApiClient
    {
        private readonly string _baseServiceUri;
        private readonly RestClient _restClient;

        public TrainingSessionApiClient(string baseServiceUri)
        {
            _baseServiceUri = baseServiceUri;
            _restClient = new RestClient(_baseServiceUri);
        }

        public async Task<int> CreateTrainingSession(TrainingSessionDTO trainingSession, string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TrainingSessionDTO>> GetTrainingSessionsByUserId(string userId, string accessToken)
        {
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddParameter("userId", userId);
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            var response = await _restClient.ExecuteAsync<IEnumerable<TrainingSessionDTO>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new Exception($"Error getting training sessions for UserId {userId}. Message was {response.StatusDescription}");
            }

            return response.Data;
        }
    }
}
