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
        public MainViewModel(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;

            Title = "Relay Commands Demo";
            CurrentUser = new UserData
            {
                Name = "Cute Kitten",
                Avatar = new Uri("http://res.freestockphotos.biz/pictures/9/9343-a-cute-orange-kitten-isolated-on-a-white-background-pv.jpg")
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
