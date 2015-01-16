using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RelayCommands.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RelayCommands.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IUserDataService _userDataService;
        private readonly IUserMessageService _userMessageService;
        public LoginViewModel(IUserService userService, 
            IUserDataService userDataService,
            IUserMessageService userMessageService)
        {
            _userService = userService;
            _userDataService = userDataService;
            _userMessageService = userMessageService;

            LogoutCommand = new RelayCommand(() =>
            {
                userService.SetUser(null);
            });
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand<string>(user =>
                    {
                        try 
                        { 
                            _userService.SetUser(_userDataService.GetData(user));
                        }
                        catch(Exception e)
                        {
                            _userMessageService.DisplayMessage("Error occurred",
                                "Could not Login with the specified user. Make sure the credentials are correct.");
                        }
                    }));
            }
        }

        public ICommand LogoutCommand
        {
            get;
            private set;
        }
    }
}
