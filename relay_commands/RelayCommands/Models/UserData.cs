using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.Models
{
    class UserData : ObservableObject
    {
        private string _userName;
        private string _email;

        public UserData(string name, string email)
        {
            UserName = name;
            Email = email;
        }
        public string UserName
        {
            get { return _userName; }
            set { Set(() => UserName, ref _userName, value); }
        }

        public string Email
        {
            get { return _email; }
            set { Set(() => Email, ref _email, value); }
        }
    }
}
