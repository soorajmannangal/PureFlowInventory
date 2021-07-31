using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class DisableInvoiceWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private NewInvoiceView window;
        public DisableInvoiceWindowCommand(NewInvoiceView invoiceWindow)
        {
            this.window = invoiceWindow;
        }

        public bool CanExecute(object parameter)
        {
            return window != null;
        }

        public void Execute(object parameter)
        {
            window.IsEnabled = false;
        }
    }
}
