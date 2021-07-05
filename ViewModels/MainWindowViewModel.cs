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

        private bool CanShowAddNewBrandWindow()
        {
            return true;
        }

        private void ShowAddNewBrandWindow()
        {
            IWindowViewModel addBrandController = new AddBrandViewModel(enableMainWindowCommand);
            var brand = new AddBrandView(addBrandController);
            disableMainWindowCommand.Execute(null);
            brand.Show();
           
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
