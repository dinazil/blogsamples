using GalaSoft.MvvmLight;
using RelayCommands.Models;
using RelayCommands.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.ViewModels
{
    class FriendsViewModel : ViewModelBase
    {
        public FriendsViewModel(IUserService userService, IUserDataService userDataService)
        {
            userService.UserChanged += (s, e) =>
                {
                    Friends.Clear();
                    if (e.NewUser != null)
                    {
                        foreach (var f in userDataService.GetFriends(e.NewUser.UserName))
                        {
                            Friends.Add(f);
                        }
                    }
                };
        }

        private ObservableCollection<UserData> _friends = new ObservableCollection<UserData>();
        public ObservableCollection<UserData> Friends
        {
            get { return _friends; }
        }
    }
}
