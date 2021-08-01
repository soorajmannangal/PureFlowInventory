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

    public partial class AddInvoiceItemView : Window
    {
        private IWindowViewModel contextViewModel;
        NewInvoiceViewModel _invoiceViewModel;
        public AddInvoiceItemView(IWindowViewModel contextViewModel, NewInvoiceViewModel invoiceViewModel)
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
