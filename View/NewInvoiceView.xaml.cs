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
    /// <summary>
    /// Interaction logic for NewInvoiceView.xaml
    /// </summary>
    public partial class NewInvoiceView : Window
    {
        private IWindowViewModel contextViewModel;
        private NewInvoiceViewModel invoiceViewModel;
        public NewInvoiceView(NewInvoiceViewModel contextViewModel)
        {
            InitializeComponent();
            this.contextViewModel = contextViewModel;
            this.DataContext = contextViewModel;
            contextViewModel.InvoiceView = this;
            invoiceViewModel = contextViewModel;
            
        }

        private void Window_OnClosed(object sender, EventArgs e)
        {
            contextViewModel.Close();
            invoiceViewModel?.UpdateInvoiceItemsGrid();
        }
    }
}
