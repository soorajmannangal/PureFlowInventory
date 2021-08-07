using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public abstract class ViewModelBase : IWindowViewModel, INotifyPropertyChanged
    {
        private ICommand enableMainWindowCommand;

        public abstract void SetDefaults();

        public ViewModelBase(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
        }

        void IWindowViewModel.Close()
        {
            enableMainWindowCommand.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }    
    }
}
