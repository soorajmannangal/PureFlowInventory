using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public enum eTableNames
    {
        Brand
    }

    public enum eGenericColumnName
    {
        ID
    }


    public interface IDataSource
    {
        void Insert(params object[] p);
        int GetId(eTableNames tableName, object columnName, object columnValue);
        List<BrandListView> GetAllBrands(string id, string name, string details, string orderBy);
    }
}
