using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RelayCommands.Models
{
    class FriendCommand : ObservableObject
    {
        public string Header { get; set; }
        public ICommand Command { get; set; }
    }
}
