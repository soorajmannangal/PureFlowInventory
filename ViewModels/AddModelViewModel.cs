using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PureFlow
{
    public class AddModelViewModel : IWindowViewModel,INotifyPropertyChanged
    {
        private readonly ICommand enableMainWindowCommand;
        private readonly ModelTable modelTable;

        public List<Dto> SimpleGrid
        {
            get
            {
                return new ModelTable().GetModelNames();
            }
            set
            {
                OnPropertyChanged("SimpleGrid");
            }
        }

        private List<Dto> brands;

        public List<Dto> Brands
        {
            get
            {
                if(brands == null)
                {
                    brands = new BrandTable().GetBrandNames(); 
                }
                return brands;
            }
        }

        private Dto selectedBrand;
        public Dto SelectedBrand
        { 
            get 
            {              
                return selectedBrand; 
            } 
            set { 
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
                modelTable.BrandID = selectedBrand.ID;
            } 
        }

        public AddModelViewModel(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
            modelTable = new ModelTable();
            SelectedBrand = Brands[0];

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

        public void SetDefaults()
        {
            Name = "";
            Details = "";
            SimpleGrid = null;
        }
        public void Close()
        {
            enableMainWindowCommand.Execute(null);
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));

        public String Name
        {
            get => modelTable.Name;
            set
            {
                modelTable.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Details
        {
            get => modelTable.Details;
            set { modelTable.Details = value; OnPropertyChanged("Details"); }
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
