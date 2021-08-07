using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class InventoryTransactionReportViewModel : ViewModelBase
    {
        private readonly InventoryTransactionTable inventoryTransactionTable;

        public InventoryTransactionReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            inventoryTransactionTable = new InventoryTransactionTable();
        }

        public List<InventoryTransactionDto> Grid
        {
            get => inventoryTransactionTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }

      
    }
}
