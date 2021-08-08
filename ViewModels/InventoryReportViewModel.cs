using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace PureFlow
{
    public class InventoryReportViewModel : ViewModelBase
    {
        private readonly InventoryTable spareInventoryTable;

        public InventoryReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            spareInventoryTable = new InventoryTable();
        }

        public ObservableCollection<InventoryDto> Grid
        {
            get => spareInventoryTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

     
    }
}
