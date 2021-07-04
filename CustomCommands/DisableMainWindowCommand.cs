using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class DisableMainWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindow mainWindow;
        public DisableMainWindowCommand(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public bool CanExecute(object parameter)
        {
            return mainWindow != null;
        }

        public void Execute(object parameter)
        {
            mainWindow.IsEnabled = false;
        }
    }
}
