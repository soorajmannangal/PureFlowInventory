using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public abstract class ViewModelBase : IWindowViewModel
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
    }
}
