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
        private int _id;
        public int ID => _id;

        private string _field;     
        public string Field => _field;
       
        public Dto(int id, string field)
        {
            _field = field;
            _id = id;
        }
    }


    public interface IDataSource
    {
        void Insert(params object[] p);
        int GetId(eTableNames tableName, object columnName, object columnValue);      
        List<Dto> GetColumnData(eTableNames tableName, string columnName);
        void UpdateSingleColumn(eTableNames tableNames, int rowId, string columnName, object columnValue);
        List<BrandGridDto> GetAllBrands(string id, string name, string details, string orderBy);
        List<SpareInventoryDto> GetAllSpares(string id, string name, string details, string quantity, string lastUpdated, string orderBy);
        List<InventoryTransactionDto> GetInventoryTransactionData(string id, string spareInventoryID, string qty, string userID, string transactionDate, string orderBy);


    }
}
