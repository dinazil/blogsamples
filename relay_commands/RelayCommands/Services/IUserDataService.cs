using RelayCommands.Models;
using RelayCommands.Services.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.Services
{
    interface IUserDataService
    {
        UserData GetData(string name);

        IEnumerable<UserData> GetFriends(string name);
    }
}
