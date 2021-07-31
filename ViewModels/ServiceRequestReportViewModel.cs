using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class ServiceRequestReportViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly ServiceRequestTable serviceRequestTable;
        public ServiceRequestReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            serviceRequestTable = new ServiceRequestTable();
        }

        public List<ServiceRequestGridDto> Grid
        {
            get => serviceRequestTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
