using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class EnableInvoiceWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private InvoiceNewView window;
        public EnableInvoiceWindowCommand(InvoiceNewView invoiceWindow)
        {
            this.window = invoiceWindow;
        }

        public bool CanExecute(object parameter)
        {
            return window != null;
        }

        public void Execute(object parameter)
        {
            this.window.IsEnabled = true;
        }
    }
}
