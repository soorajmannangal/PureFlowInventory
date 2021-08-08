using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class ModelTable : ORMBase
    {
        private int brandId;
        public int BrandID { get { return brandId; } set { brandId = value; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string details;
        public string Details { get { return details ?? DEFAULT_STRING; } set { details = value; } }

        public override eTableNames TableName => eTableNames.ModelsTable;

        public int GetIdByName()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name);
        }

        public bool IsModelNameExist()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name) != 0;
        }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(Name), Name, nameof(Details), Details, nameof(BrandID), BrandID);          
        }
   
        public List<ComboDto> GetModelNames()
        {
            return _dataSource.GetColumnData(TableName, nameof(Name));
        }

        public List<ComboDto> GetModelNames(int brandId)
        {
            this.BrandID = brandId;
            return _dataSource.GetColumnDataByFKId(TableName, nameof(Name), nameof(BrandID), brandId);
        }

        public List<ComboDto> GetNamesById(int id)
        {
            return _dataSource.GetColumnDataById(TableName, nameof(Name), id);
        }


        public bool IsValidForInsert()
        {
            if (String.IsNullOrEmpty(Name)) return false;
            if (BrandID <= 0) return false;
            return true;
        }

        private List<ModelDto> GetModelBrandData()
        {
            return new List<ModelDto>();
        }

        internal List<ComboDto> GetModelNamesById(int modelID)
        {
            throw new NotImplementedException();
        }
    }
}
