using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class BrandTable : ORMBase
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string details;
        public string Details { get { return details ?? DEFAULT_STRING; } set { details = value; } }
      
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
             _dataSource.Insert(TableName, nameof(Name), Name, nameof(Details), Details);
        }

        public List<BrandGridDto> GetAllBrands()
        {
           return _dataSource.GetAllBrands(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Details), nameof(Name));
        }

        public List<Dto> GetBrandNames()
        {
            return _dataSource.GetColumnData(TableName, nameof(Name));
        }

        public bool IsValidForInsert()
        {
            if (String.IsNullOrEmpty(Name)) return false;
            return true;
        }

       
    }
}
