using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class ServiceManTable : ORMBase
    {      
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string phone;
        public string Phone { get { return phone; } set { phone = value; } }

        private string details;
        public string Details { get { return details ?? DEFAULT_STRING; } set { details = value; } }

        private int isActive;
        public int IsActive { get => isActive; set { isActive = value; } }

        public ServiceManTable()
        {
            isActive = 1;
        }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(Name), Name, nameof(Details), Details, nameof(Phone), Phone, nameof(IsActive), IsActive);
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

        public override eTableNames TableName => eTableNames.ServiceMan;
    }
}
