using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public enum eTableNames
    {
        Brand,
        Customer,
        InventoryTransaction,
        Invoice,
        Model,
        ServiceMan,
        ServiceReminder,
        ServiceRequest,
        SpareInventory,
        User,
        WorkType
    }

    public enum eGenericColumnName
    {
        ID
    }

    public class Dto
    {
        private string _field;
        public string Field => _field;
        public Dto(string field)
        {
            _field = field;
        }
    }


    public interface IDataSource
    {
        void Insert(params object[] p);
        int GetId(eTableNames tableName, object columnName, object columnValue);
        List<BrandGridDto> GetAllBrands(string id, string name, string details, string orderBy);
        List<Dto> GetColumnData(eTableNames tableName, string columnName);
    }
}
