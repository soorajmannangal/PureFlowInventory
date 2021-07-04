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
using PureFlow.Controllers;

namespace PureFlow
{
    /// <summary>
    /// Interaction logic for AddNewBrand.xaml
    /// </summary>
    public partial class AddBrandView : Window
    {
        private IWindowController windowController; 
        public AddBrandView(IWindowController windowController)
        {
            InitializeComponent();
            this.windowController = windowController;
            this.DataContext = windowController;
        }

        private void AddBrandView_OnClosed(object sender, EventArgs e)
        {
            windowController.Close();
        }
    }
}
