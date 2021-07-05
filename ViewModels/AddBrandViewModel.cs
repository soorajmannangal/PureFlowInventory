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
        private List<BrandListView> gridBrandList;

        public List<BrandListView> GridBrandListView
        {
            get => gridBrandList;
            set
            {
                //gridBrandList = value;
                NotifyPropertyChanged();
            }
        }
        public class ArticleItem
        {
            public int ID { get; set; }
            public int ViewCount { get; set; }
            public String Title { get; set; }
        }

       

        public AddBrandViewModel(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
            brandTable = new BrandTable();
            gridBrandList = brandTable.GetAllBrands();
         }

        private bool CanAddNewBrand()
        {
            if (!brandTable.IsValidForInsert()) return false;

            return !brandTable.IsBrandNameExist();
        }

        private void AddNewBrand()
        {
            //gridBrandList.Add(new BrandListView() { ID = 2, Name = "Kelvinator2", Details = "NA" });
            //gridBrandList.Add(new BrandListView(2,"test","test"));
            GridBrandListView = null;
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
