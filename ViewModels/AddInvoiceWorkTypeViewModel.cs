﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace PureFlow
{
    public class AddInvoiceWorkTypeViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly WorkTypeTable workTypeTable;

        public AddInvoiceWorkTypeViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            workTypeTable = new WorkTypeTable();
        }

        public override void SetDefaults()
        {
            Name = "";      
            Grid = null;
        }

        public List<Dto> Grid
        {
            get => new WorkTypeTable().GetItemNames();
            set => OnPropertyChanged("Grid");
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(SetDefaults));

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {
            workTypeTable.InsertAll();
            SetDefaults();
        }

        private bool CanAddNew()
        {
            if (!workTypeTable.IsValidForInsert()) return false;

            return !workTypeTable.IsItemNameExist();
        }

        public String Name
        {
            get => workTypeTable.Name;
            set
            {
                workTypeTable.Name = value;
                OnPropertyChanged("Name");
            }
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
