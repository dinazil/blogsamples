using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelayCommands.Services
{
    class UserMessageService : IUserMessageService
    {
        public void DisplayMessage(string title, string content)
        {
            MessageBox.Show(content, title);
        }
    }
}
