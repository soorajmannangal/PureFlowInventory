using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace PureFlow
{
    public class InventoryAddViewModel : ViewModelBase
    {
        private readonly InventoryTable spareInventoryTable;

        public InventoryAddViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            spareInventoryTable = new InventoryTable();
        }

        public override void SetDefaults()
        {
            Name = "";
            Details = "";
            Grid = null;
            Quantity = 0;
        }

        public ObservableCollection<InventoryDto> Grid
        {
            get => spareInventoryTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {
            spareInventoryTable.InsertAll();
            var invTrans = new InventoryTransactionTable(spareInventoryTable.GetId(), spareInventoryTable.Quantity, UserInfo.GetInstance().UserID);
            invTrans.InsertAll();

            SetDefaults();
        }

        private bool CanAddNew()
        {
            if (!spareInventoryTable.IsValidForInsert()) return false;

            return !spareInventoryTable.IsItemNameExist();
        }

        public String Name
        {
            get => spareInventoryTable.Name;
            set
            {
                spareInventoryTable.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Details
        {
            get => spareInventoryTable.Details;
            set { spareInventoryTable.Details = value; OnPropertyChanged("Details"); }
        }

        public int Quantity { get { return spareInventoryTable.Quantity; } set { spareInventoryTable.Quantity = value; OnPropertyChanged("Quantity"); } }     

       
    }
}
