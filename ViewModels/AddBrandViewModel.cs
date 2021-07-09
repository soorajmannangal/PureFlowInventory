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
             
        public List<BrandGridDto> Grid => new BrandGrid().Grid;
        public List<Dto> SimpleGrid => new BrandTable().GetBrandNames();
      
        public AddBrandViewModel(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
            brandTable = new BrandTable();
        }

        private bool CanAddNewBrand()
        {
            if (!brandTable.IsValidForInsert()) return false;

            return !brandTable.IsBrandNameExist();
        }

        private void AddNewBrand()
        {
            brandTable.InsertAll();
            SetDefaults();
        }

        public void SetDefaults()
        {                 
            Name = "";
            Description = "";
        }
        public void Close()
        {
            enableMainWindowCommand.Execute(null);
        }

        private ICommand addNewBrandCommand;
        public ICommand AddNewBrandCommand => addNewBrandCommand ?? (addNewBrandCommand = new RelayCommand(AddNewBrand, CanAddNewBrand));

        public String Name
        {
            get => brandTable.Name;
            set
            {
                brandTable.Name = value;
                NotifyPropertyChanged();
            }
        }

        public String Description
        {
            get => brandTable.Details;
            set { brandTable.Details = value; NotifyPropertyChanged(); }
        }      

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }

    }
}
