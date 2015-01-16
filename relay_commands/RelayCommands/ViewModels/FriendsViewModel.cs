using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

            Commands.Add(new FriendCommand
                {
                    Header = "Remove",
                    Command = new RelayCommand<UserData>(user =>
                    {
                        // userDataService.remove(currentUser, user);
                        Friends.Remove(user);
                    })
                });
        }

        private UserData _currentFriend;
        public UserData CurrentFriend
        {
            get { return _currentFriend; }
            set { Set(() => CurrentFriend, ref _currentFriend, value); }
        }

        private ObservableCollection<UserData> _friends = new ObservableCollection<UserData>();
        public ObservableCollection<UserData> Friends
        {
            get { return _friends; }
        }

        private ObservableCollection<FriendCommand> _commands = new ObservableCollection<FriendCommand>();
        public ObservableCollection<FriendCommand> Commands
        {
            get { return _commands; }
        }
    }
}
