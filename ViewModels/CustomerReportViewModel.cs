using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class CustomerReportViewModel : ViewModelBase
    {
        private readonly CustomerTable customerTable;

        public CustomerReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            customerTable = new CustomerTable();
        }

        public List<CustomerDto> Grid
        {
            get => customerTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

    }
}
