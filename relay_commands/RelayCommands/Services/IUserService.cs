using RelayCommands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.Services
{
    interface IUserService
    {
        void SetUser(UserData user);

        UserData GetCurrentUser();

        event EventHandler<UserChangedEventArgs> UserChanged;
    }
}
