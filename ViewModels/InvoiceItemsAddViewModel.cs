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
    public class InvoiceItemsAddViewModel : ViewModelBase
    {
        private readonly InventoryTable spareInventoryTable;
        InvoiceNewViewModel _invoiceViewModel;
        public InvoiceItemsAddViewModel(ICommand enableMainWindowCommand, InvoiceNewViewModel invoiceViewModel) : base(enableMainWindowCommand)
        {
            _invoiceViewModel = invoiceViewModel;
            spareInventoryTable = new InventoryTable();
            LoadData();
            SetDefaults();
        }

        private void LoadData()
        {
            Items = spareInventoryTable.GetInventoryItemsWithStock();

            if (Items.Count > 0)
            {
                //check for stock
                foreach (var item in Items)
                {
                    int prodId = item.ID;
                    if(_invoiceViewModel.usedStock.ContainsKey(prodId))
                    {
                        int used = 0;
                        _invoiceViewModel.usedStock.TryGetValue(prodId,out used);
                        int balance = item.Quantity - used;
                        item.Quantity = balance;
                        if(balance <= 0)
                        {
                            Items.Remove(item);
                            if (Items.Count == 0)
                            {
                                Qty = null;
                                break;
                            }
                        }
                    }
                }


                if(Items.Count > 0)
                    SelectedItem = Items[0];
            }

            ServiceDetail = new WorkTypeTable().GetItemNames();
            if (ServiceDetail.Count > 0)
            {
                SelectedServiceDetail = ServiceDetail[0];
            }
        }

        

        private List<ComboDto> serviceDetail;
        public List<ComboDto> ServiceDetail
        {
            get
            {
                return serviceDetail;
            }
            set
            {
                serviceDetail = value;
                OnPropertyChanged("ServiceDetail");
            }
        }

        private ComboDto selectedServiceDetail;
        public ComboDto SelectedServiceDetail
        {
            get => selectedServiceDetail;
            set
            {
                selectedServiceDetail = value;
                OnPropertyChanged("SelectedServiceDetail");       
            }
        }


        private List<InventoryDto> items;
        public List<InventoryDto> Items
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

        private InventoryDto selectedItem;
        public InventoryDto SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
                Amount = 0;
                PrepareQty();
            }
        }

        void PrepareQty()
        {
            if (SelectedItem == null) return;

            var lst = new List<ComboDto>();
            for(int i=1; i <= SelectedItem.Quantity; i++)
            {
                lst.Add(new ComboDto(i, ""));
            }

            Qty = lst;
            if(lst.Count > 0)
            {
                SelectedQty = lst[0];
            }
        }

        private List<ComboDto> qty;
        public List<ComboDto> Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
                OnPropertyChanged("Qty");
            }
        }

        private ComboDto selectedQty;
        public ComboDto SelectedQty
        {
            get => selectedQty;
            set
            {
                selectedQty = value;
                SetDefaults();
                OnPropertyChanged("SelectedQty");
            }
        }

     
        public override void SetDefaults()
        {
            Amount = 0;
           
        }

        private ICommand addItemCommand;
        public ICommand AddItemCommand => addItemCommand ?? (addItemCommand = new RelayCommand(AddNew, CanAddNew));
        private void AddNew()
        {

            if(SelectedQty == null || SelectedItem == null)
            {
                return;
            }

            int index = _invoiceViewModel.InvoiceItems.Count + 1;
            _invoiceViewModel.InvoiceItems.Add(new InvoiceItemsDto()
            {
                Ind = index,
                ItemName = SelectedItem.Name,
                InventoryID = SelectedItem.ID,
                Qty = SelectedQty.ID,
                WorkTypeID = SelectedServiceDetail.ID,
                ServiceType = SelectedServiceDetail.Field,
                Amount = amount
            });

            if (_invoiceViewModel.usedStock.ContainsKey(SelectedItem.ID))
            {
                int used = 0;
                _invoiceViewModel.usedStock.TryGetValue(SelectedItem.ID, out used);
                int newStock = SelectedQty.ID + used;
                _invoiceViewModel.usedStock[SelectedItem.ID] = newStock;
            }
            else
            {
                _invoiceViewModel.usedStock.Add(SelectedItem.ID, SelectedQty.ID);
            }

            LoadData();
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

    }
}
