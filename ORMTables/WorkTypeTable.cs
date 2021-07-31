using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class WorkTypeTable :ORMBase
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(Name), Name);
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

        public override eTableNames TableName => eTableNames.WorkType;
    }
}
