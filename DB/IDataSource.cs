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


    public interface IDataSource
    {
        void Insert(params object[] p);
        int GetId(eTableNames tableName, object columnName, object columnValue);
        List<BrandGrid> GetAllBrands(string id, string name, string details, string orderBy);
        List<String> GetColumnData(eTableNames tableName, string columnName);
    }
}
