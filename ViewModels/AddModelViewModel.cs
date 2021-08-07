using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class AddModelViewModel : ViewModelBase
    {
        private readonly ModelTable modelTable;

        public List<ComboDto> SimpleGrid
        {
            get => new ModelTable().GetModelNames();
            set => OnPropertyChanged("SimpleGrid");
        }

        private List<ComboDto> brands;
        public List<ComboDto> Brands => brands ?? (brands = new BrandTable().GetBrandNames());

        private ComboDto selectedBrand;
        public ComboDto SelectedBrand
        {
            get => selectedBrand;
            set 
            { 
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
                modelTable.BrandID = selectedBrand.ID;
                SetDefaults();
            } 
        }

        public AddModelViewModel(ICommand enableMainWindowCommand):base(enableMainWindowCommand)
        {
            modelTable = new ModelTable();
            if (Brands.Count > 0)
            {
                SelectedBrand = Brands[0];
            }
        }

        private bool CanAddNew()
        {          
            if (!modelTable.IsValidForInsert()) return false;
            return !modelTable.IsModelNameExist();
        }

        private void AddNew()
        {
            modelTable.InsertAll();
            SetDefaults();
        }

        public override void SetDefaults()
        {
            Name = "";
            Details = "";
            SimpleGrid = null;
        }
       
        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));

        public String Name
        {
            get => modelTable.Name;
            set { 
                modelTable.Name = value; 
                OnPropertyChanged("Name");}
        }

        public String Details
        {
            get => modelTable.Details;
            set { modelTable.Details = value; OnPropertyChanged("Details"); }
        }

      
    }
}
