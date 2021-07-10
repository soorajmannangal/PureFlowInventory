using System;
using System.Collections.Generic;
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

        private DateTime lastUpdatedDate;
        public DateTime LastUpdated { get => lastUpdatedDate; set { lastUpdatedDate = value; } }

        public SpareInventoryTable()
        {
            lastUpdatedDate = DateTime.Now;
        }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(Name), Name, nameof(Details), Details, nameof(Quantity), Quantity, nameof(LastUpdated), LastUpdated);
        }

        public List<Dto> GetItemNames()
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

        public override eTableNames TableName => eTableNames.SpareInventory;
    }
}
