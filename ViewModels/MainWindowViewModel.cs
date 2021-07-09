using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using PureFlow;

namespace PureFlow
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private MainWindow mainWindow;
        private ICommand showAddNewBrandWindowCommand;
        private ICommand showAddNewModelWindowCommand;
        private ICommand disableMainWindowCommand;
        private ICommand enableMainWindowCommand;
     
        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            CreateCustomCommands();
        }

        private void CreateCustomCommands()
        {
            disableMainWindowCommand = new DisableMainWindowCommand(mainWindow);
            enableMainWindowCommand = new EnableMainWindowCommand(mainWindow);
        }

        public ICommand ShowAddNewBrandWindowCommand => showAddNewBrandWindowCommand ?? (showAddNewBrandWindowCommand = new RelayCommand(ShowAddNewBrandWindow, CanShowAddNewBrandWindow));
        public ICommand ShowAddNewModelWindowCommand => showAddNewModelWindowCommand ?? (showAddNewModelWindowCommand = new RelayCommand(ShowAddNewModelWindow, CanShowAddNewModelWindow));

        private bool CanShowAddNewModelWindow()
        {
            return true;
        }

        private void ShowAddNewModelWindow()
        {
            IWindowViewModel contextViewModel = new AddModelViewModel(enableMainWindowCommand);
            var contextView = new AddModelView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();
        }

        private bool CanShowAddNewBrandWindow()
        {
            return true;
        }

        private void ShowAddNewBrandWindow()
        {
            IWindowViewModel contextViewModel = new AddBrandViewModel(enableMainWindowCommand);
            var contextView = new AddBrandView(contextViewModel);
            disableMainWindowCommand.Execute(null);
            contextView.Show();        
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
