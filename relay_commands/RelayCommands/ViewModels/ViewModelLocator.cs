using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RelayCommands.Services;
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
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}
