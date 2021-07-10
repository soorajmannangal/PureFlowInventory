using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace PureFlow
{
    public class AddSpareInventoryViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly SpareInventoryTable spareInventoryTable;

        public AddSpareInventoryViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            spareInventoryTable = new SpareInventoryTable();
        }

        public override void SetDefaults()
        {
            Name = "";
            Details = "";
            Grid = null;
        }

        public List<Dto> Grid
        {
            get => new SpareInventoryTable().GetItemNames();
            set => OnPropertyChanged("Grid");
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {
            spareInventoryTable.InsertAll();
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
