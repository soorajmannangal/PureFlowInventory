using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class AddBrandViewModel : IWindowViewModel,INotifyPropertyChanged
    {      
        private readonly ICommand enableMainWindowCommand;
        private readonly BrandTable brandTable;

        public List<BrandGridDto> Grid
        {
            get => brandTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        public AddBrandViewModel(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
            brandTable = new BrandTable();
        }

        private bool CanAddNew()
        {
            if (!brandTable.IsValidForInsert()) return false;

            return !brandTable.IsBrandNameExist();
        }

        private void AddNew()
        {
            brandTable.InsertAll();
            SetDefaults();
        }

        public void SetDefaults()
        {                 
            Name = "";
            Details = "";
            Grid = null;
        }

        public void Close()
        {
            enableMainWindowCommand.Execute(null);
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));

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
