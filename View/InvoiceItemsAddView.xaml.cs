using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PureFlow
{

    public partial class InvoiceItemsAddView : Window
    {
        private IWindowViewModel contextViewModel;
        InvoiceNewViewModel _invoiceViewModel;
        public InvoiceItemsAddView(IWindowViewModel contextViewModel, InvoiceNewViewModel invoiceViewModel)
        {
            InitializeComponent();
            this.contextViewModel = contextViewModel;
            this.DataContext = contextViewModel;
            _invoiceViewModel = invoiceViewModel;
        }

        private void Window_OnClosed(object sender, EventArgs e)
        {
            contextViewModel.Close();
            _invoiceViewModel.UpdateInvoiceItemsGrid();
        }

       
    }
}
