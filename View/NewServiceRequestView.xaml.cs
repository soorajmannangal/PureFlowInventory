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
    /// Interaction logic for NewServiceRequestView.xaml
    /// </summary>
    public partial class NewServiceRequestView : Window
    {
        private IWindowViewModel contextViewModel;
        public NewServiceRequestView(IWindowViewModel contextViewModel)
        {
            InitializeComponent();
            this.contextViewModel = contextViewModel;
            this.DataContext = contextViewModel;
        }

        private void Window_OnClosed(object sender, EventArgs e)
        {
            contextViewModel.Close();
        }
    }
}
