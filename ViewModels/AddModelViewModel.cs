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
    public class AddModelViewModel : IWindowViewModel,INotifyPropertyChanged
    {      
        private readonly ICommand enableMainWindowCommand;
        private readonly BrandTable brandTable;

        private List<ModelsGrid> gridModels;
        public List<ModelsGrid> GridModels
        {
            get => gridModels;
            set
            {
                if (gridModels != null)
                    gridModels.AddRange(value);
                else
                    gridModels = value;
                NotifyPropertyChanged();
            }
        }

        private List<string> brandNameCombo;
        public List<String> BrandNameCombo
        {
            get => brandNameCombo;
            set
            {
                brandNameCombo = value;
                NotifyPropertyChanged();
            }
        }



        public AddModelViewModel(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
            brandTable = new BrandTable();
            SetDefaults();
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
            //gridBrandList = brandTable.GetAllBrands();
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
