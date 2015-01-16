using RelayCommands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelayCommands.Services
{
    class UserChangedEventArgs : EventArgs
    {
        public UserData OldUser { get; private set; }
        public UserData NewUser { get; private set; }
        public UserChangedEventArgs(UserData oldUser, UserData newUser)
        {
            OldUser = oldUser;
            NewUser = newUser;
        }
    }
}
