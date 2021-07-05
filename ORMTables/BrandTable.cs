using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class BrandTable : ORMBase
    {      
        public string Name { get; set; }
        public string Details { get; set; }
      
        public override eTableNames TableName => eTableNames.Brand;

        public int GetIdByName()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name);
        }

        public bool IsBrandNameExist()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name) != 0;
        }

        public void InsertAll()
        {
            SetMissingAsDefaults();
            _dataSource.Insert(TableName, nameof(Name), Name, nameof(Details), Details);
        }

        public bool IsValidForInsert()
        {
            if (String.IsNullOrEmpty(Name)) return false;
            return true;
        }

        public void SetMissingAsDefaults()
        {
            if(string.IsNullOrEmpty(Details))
            {
                Details = DEFAULT_STRING;
            }
        }
    }
}
