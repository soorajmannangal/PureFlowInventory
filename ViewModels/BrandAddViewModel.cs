using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class BrandAddViewModel : ViewModelBase
    {      
        private readonly BrandTable brandTable;
    
        public BrandAddViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
              brandTable = new BrandTable();
        }

        public override void SetDefaults()
        {
            Name = "";
            Details = "";
            Grid = null;
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {
            brandTable.InsertAll();
            SetDefaults();
        }
        private bool CanAddNew()
        {
            if (!brandTable.IsValidForInsert()) return false;

            return !brandTable.IsBrandNameExist();
        }
     
        public String Name
        {
            get => brandTable.Name;
            set
            {
                brandTable.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Details
        {
            get => brandTable.Details;
            set { brandTable.Details = value; OnPropertyChanged("Details"); }
        }

        public ObservableCollection<BrandDto> Grid
        {
            get => brandTable.Grid;
            set => OnPropertyChanged("Grid");
        }
    }
}
