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
    public class AddInvoiceItemViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly SpareInventoryTable spareInventoryTable;
        public AddInvoiceItemViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            spareInventoryTable = new SpareInventoryTable();
            Items = spareInventoryTable.GetInventoryItems();
            if (Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
            
        }

        private List<SpareInventoryDto> items;
        public List<SpareInventoryDto> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        private SpareInventoryDto selectedItem;
        public SpareInventoryDto SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
                SetDefaults();
            }
        }

        public int Quantity { get { return spareInventoryTable.Quantity; } set { spareInventoryTable.Quantity = value; OnPropertyChanged("Quantity"); } }


        public override void SetDefaults()
        {
            Amount = 0;         
        }

        private ICommand addItemCommand;
        public ICommand AddItemCommand => addItemCommand ?? (addItemCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {
            SetDefaults();
        }

        private bool CanAddNew()
        {
            return true;
        }
     
        int amount;
        public int Amount
        {
            get => amount;
            set { amount = value; OnPropertyChanged("Amount"); }
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
