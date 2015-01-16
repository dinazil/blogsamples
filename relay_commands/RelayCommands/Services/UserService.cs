using RelayCommands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.Services
{
    class UserService : IUserService
    {
        private UserData _current;

        public void SetUser(UserData user)
        {
            if (_current != user)
            {
                var old = _current;
                _current = user;
                if (UserChanged != null)
                {
                    UserChanged(this, new UserChangedEventArgs(old, _current));
                }
            }
        }

        public UserData GetCurrentUser()
        {
            return _current;
        }

        public event EventHandler<UserChangedEventArgs> UserChanged;
    }
}
