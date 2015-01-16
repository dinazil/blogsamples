using RelayCommands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.Services.Simulation
{
    class UserDataServie : IUserDataService
    {
        private static Dictionary<string, UserData> users = new Dictionary<string, UserData>
        {
            {"svetlana", new UserData("svetlana", "svetlana@gmail.com")},
            {"igor", new UserData("igor", "igor@hotmail.com")},
            {"billy", new UserData("billy", "bill@outlook.com")},
            {"masha", new UserData("masha", "maria85@netvision.co.il")}
        };

        private static Dictionary<string, List<string>> friends = new Dictionary<string, List<string>>
        {
            {"svetlana", new List<string>{"igor", "billy"}},
            {"igor", new List<string>{"svetlana", "billy", "masha"}},
            {"billy", new List<string>{"svetlana", "igor"}},
            {"masha", new List<string>{"igor"}}
        };

        public UserData GetData(string name)
        {
            return users[name];
        }

        public IEnumerable<UserData> GetFriends(string name)
        {
            return friends[name].Select(n => users[n]);
        }
    }
}
