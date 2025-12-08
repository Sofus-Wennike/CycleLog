using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> GetUsernameAsync(string sub);
    }
}
