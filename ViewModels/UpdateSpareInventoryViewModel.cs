﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace PureFlow
{
    public class UpdateSpareInventoryViewModel : ViewModelBase
    {
        private readonly SpareInventoryTable spareInventoryTable;
        
        public UpdateSpareInventoryViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            spareInventoryTable = new SpareInventoryTable();
            if (Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
        }

        public ObservableCollection<SpareInventoryDto> Grid
        {
            get => spareInventoryTable.Grid;
            set => OnPropertyChanged("Grid");
        }

        private ObservableCollection<SpareInventoryDto> items;
        public ObservableCollection<SpareInventoryDto> Items => items ?? (items = spareInventoryTable.Grid);

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

        public String Details
        {
            get => spareInventoryTable.Details;
            set { spareInventoryTable.Details = value; OnPropertyChanged("Details"); }
        }

        public int Quantity { get { return spareInventoryTable.Quantity; } set { spareInventoryTable.Quantity = value; OnPropertyChanged("Quantity"); } }

        private int updateQty;
        public int UpdateQty { get { return updateQty; } set { updateQty = value; OnPropertyChanged("UpdateQty"); } }

        public override void SetDefaults()
        {
            Details = SelectedItem.Details;
            Quantity = SelectedItem.Quantity;
            UpdateQty = 0;
            Grid = null;
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand => updateCommand ?? (updateCommand = new RelayCommand(Update, CanUpdate));
        private void Update()
        {          
            spareInventoryTable.UpdateQty(SelectedItem.ID, UpdateQty);
            var invTrans = new InventoryTransactionTable(SelectedItem.ID, UpdateQty, UserInfo.GetInstance().UserID);
            invTrans.InsertAll();
            SetDefaults();
        }

        private bool CanUpdate()
        {
            return true;
        }
      
    }
}
