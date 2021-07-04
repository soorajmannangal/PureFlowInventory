using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PureFlow.Controllers;
using PureFlowSystems;

namespace PureFlow
{
    public class AddBrandViewModel : IWindowController,INotifyPropertyChanged
    {
        
        private readonly ICommand enableMainWindowCommand;

        public AddBrandViewModel(ICommand enableMainWindowCommand)
        {
            this.enableMainWindowCommand = enableMainWindowCommand;
        }

        private bool CanAddNewBrand()
        {
            if (string.IsNullOrEmpty(name)) return false;

            return !DBHelper.GetInstance().IsBrandNameExist(Name);
        }

        private void AddNewBrand()
        {
            DBHelper.GetInstance().InsertBrand(Name, Description);
            Name = "";
            Description= "";
        }




        private ICommand addNewBrandCommand;
        public ICommand AddNewBrandCommand => addNewBrandCommand ?? (addNewBrandCommand = new RelayCommand(AddNewBrand, CanAddNewBrand));

        private String name;
        public String Name
        {
            get => this.name;
            set
            {
                this.name = value;
                NotifyPropertyChanged();
            }
        }

        private String description;
        public String Description
        {
            get => description;
            set { this.description = value; NotifyPropertyChanged(); }
        }

        public void Close()
        {
            enableMainWindowCommand.Execute(null);
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
