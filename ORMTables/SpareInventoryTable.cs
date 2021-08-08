using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class SpareInventoryTable : ORMBase
    {      
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private int quantity;
        public int Quantity { get { return quantity; } set { quantity = value; } }

        private string details;
        public string Details { get { return details ?? DEFAULT_STRING; } set { details = value; } }

       
        public ObservableCollection<SpareInventoryDto> Grid => _dataSource.GetAllSpares(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Details), nameof(Quantity), nameof(Name));


        public ObservableCollection<SpareInventoryDto> GetInventoryItems()
        {
            return _dataSource.GetAllSpares(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Details), nameof(Quantity), nameof(Name));
        }

        public List<SpareInventoryDto> GetInventoryItemsWithStock()
        {
            return _dataSource.GetAllSparesWithStock(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Details), nameof(Quantity), nameof(Name));
        }

        public SpareInventoryTable()
        {
         
        }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(Name), Name, nameof(Details), Details, nameof(Quantity), Quantity);
        }

        public void UpdateQty(int id, int newQuantity)
        {

            int currentValue = _dataSource.GetSingleColumnValueById(TableName, id, nameof(Quantity));
            _dataSource.UpdateSingleColumn(TableName, id, nameof(Quantity), newQuantity + currentValue);
        }



        public List<ComboDto> GetItemNames()
        {
            return _dataSource.GetColumnData(TableName, nameof(Name));
        }

        public bool IsValidForInsert()
        {
            if (String.IsNullOrEmpty(Name)) return false;
            return true;
        }

        public bool IsItemNameExist()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name) != 0;
        }

        public int GetId()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name);
        }

        public override eTableNames TableName => eTableNames.SpareInventory;
    }
}
