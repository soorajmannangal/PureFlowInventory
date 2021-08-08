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
    public class TechnicianAddViewModel: ViewModelBase
    {
        private readonly TechnicianTable serviceManTable;

        public TechnicianAddViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            serviceManTable = new TechnicianTable();
        }

        public override void SetDefaults()
        {
            Name = "";
            Details = "";
            Phone = "";
            Grid = null;
        }

        public List<ComboDto> Grid
        {
            get => new TechnicianTable().GetItemNames();
            set => OnPropertyChanged("Grid");
        }

        private ICommand addNewCommand;
        public ICommand AddNewCommand => addNewCommand ?? (addNewCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {

            serviceManTable.InsertAll();
            SetDefaults();
        }

        private bool CanAddNew()
        {
            if (!serviceManTable.IsValidForInsert()) return false;

            return !serviceManTable.IsItemNameExist();
        }

        public String Name
        {
            get => serviceManTable.Name;
            set
            {
                serviceManTable.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Details
        {
            get => serviceManTable.Details;
            set { serviceManTable.Details = value; OnPropertyChanged("Details"); }
        }

        public String Phone
        {
            get => serviceManTable.Phone;
            set { serviceManTable.Phone = value; OnPropertyChanged("Phone"); }
        }

    }
}

