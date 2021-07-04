using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class EnableMainWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindow mainWindow;
        public EnableMainWindowCommand(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public bool CanExecute(object parameter)
        {
            return mainWindow != null;
        }

        public void Execute(object parameter)
        {
            this.mainWindow.IsEnabled = true;
        }
    }
}
