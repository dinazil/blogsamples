using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RelayCommands.Services;
using RelayCommands.Services.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.ViewModels
{
    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IUserMessageService, UserMessageService>();
            SimpleIoc.Default.Register<IUserDataService, UserDataService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<FriendsViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public FriendsViewModel FriendsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FriendsViewModel>();
            }
        }
    }
}
