using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class ServiceRequestReportViewModel : ViewModelBase
    {
        private readonly ServiceRequestTable serviceRequestTable;
        public ServiceRequestReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            serviceRequestTable = new ServiceRequestTable();
        }

        public List<ServiceRequestDto> Grid
        {
            get => serviceRequestTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

       
    }
}
