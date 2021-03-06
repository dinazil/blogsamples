﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RelayCommands.Models;
using RelayCommands.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RelayCommands.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel(IUserService userService,
            IUserMessageService userMessageService,
            FriendsViewModel friendsViewModel)
        {
            Title = "Relay Commands Demo";

            userService.UserChanged += (s, e) =>
                {
                    CurrentUser = e.NewUser;
                };

            friendsViewModel.Commands.Add(new FriendCommand
                {
                    Header = "Login with this user",
                    Command = new RelayCommand<UserData>(user=>
                    {
                        userService.SetUser(user);
                    })
                });

            GCCommand = new RelayCommand(() =>
            {
                GC.Collect();
                userMessageService.DisplayMessage("Operation Complete", "Ran GC successfully");
            });
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(() => Title, ref _title, value); }
        }

        private UserData _currentUser;
        public UserData CurrentUser
        {
            get { return _currentUser; }
            set { Set(() => CurrentUser, ref _currentUser, value); }
        }

        public ICommand GCCommand
        {
            get;
            private set;
        }
    }
}
