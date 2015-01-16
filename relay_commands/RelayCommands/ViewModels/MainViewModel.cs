using GalaSoft.MvvmLight;
using RelayCommands.Models;
using RelayCommands.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly IUserMessageService _userMessageService;
        public MainViewModel(IUserService userService,
            IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;

            Title = "Relay Commands Demo";

            userService.UserChanged += (s, e) =>
                {
                    CurrentUser = e.NewUser;
                };
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
    }
}
