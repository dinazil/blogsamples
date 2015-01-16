using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayCommands.Services
{
    interface IUserMessageService
    {
        void DisplayMessage(string title, string content);
    }
}
