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
        private string _name;
        private Uri _avatar;

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        public Uri Avatar
        {
            get { return _avatar; }
            set { Set(() => Avatar, ref _avatar, value); }
        }
    }
}
