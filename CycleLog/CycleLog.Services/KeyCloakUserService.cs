using CycleLog.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.Services
{
    public class KeyCloakUserService : IUserService
    {
        private readonly string _baseServiceUri;
        private readonly RestClient _restClient;

        public KeyCloakUserService(string baseServiceUri)
        {
            _baseServiceUri = baseServiceUri;
            _restClient = new RestClient(_baseServiceUri);
        }

        public Task<string> GetUsernameAsync(string sub)
        {
            throw new NotImplementedException();
        }
    }
}
