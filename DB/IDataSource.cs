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
        WorkType,
        InvoiceItems
    }

    public enum ServiceRequestStatus
    {
        RequestPlaced,
        RequestResolved,
        RequestRejected
    }

    public enum eGenericColumnName
    {
        ID
    }
   
    public class ComboDto
    {
        private int _id;
        public int ID => _id;

        private string _field;     
        public string Field => _field;
       
        public ComboDto(int id, string field)
        {
            _field = field;
            _id = id;
        }
    }


    public interface IDataSource
    {
        void Insert(params object[] p);
        int GetId(eTableNames tableName, object columnName, object columnValue);
        List<ComboDto> GetColumnData(eTableNames tableName, string columnName);
        List<ComboDto> GetColumnDataById(eTableNames tableName, string columnName, int id);
        int GetLastId(eTableNames tableName, string columnName);
        List<ComboDto> GetColumnDataByFKId(eTableNames tableName, string columnName, string fkIdColumnName, int fkIdValue);
        void UpdateSingleColumn(eTableNames tableNames, int rowId, string columnName, object columnValue);
        int GetSingleColumnValueById(eTableNames tableName, int rowId, string columnName);
        List<BrandGridDto> GetAllBrands(string id, string name, string details, string orderBy);
        List<SpareInventoryDto> GetAllSpares(string id, string name, string details, string quantity, string orderBy);
        List<SpareInventoryDto> GetAllSparesWithStock(string id, string name, string details, string quantity, string orderBy);
        List<ServiceRequestGridDto> GetServiceRequestList(string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string orderBy);
        List<InventoryTransactionDto> GetInventoryTransactionData(string id, string spareInventoryID, string qty, string userID, string transactionDate, string orderBy);
        List<CustomerGridDto> GetAllCustomers(string id, string name, string phone, string address, string email, string orderBy);
        CustomerGridDto GetCustomerByPhone(string id, string name, string phone, string address, string email, string phoneNoToMatch);
        List<ServiceRequestGridDto> GetServiceRequestListForCustomerId(string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string orderBy, int customerId);
        List<InvoiceGridDto> GetAllInvoices(string id, string customerID, string nvoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string note, string orderBy);
        List<InvoiceItemsGridDto> GetInvoiceItems(string id, string invoiceID, string spareInventoryID, string qty, string workTypeID, string amount, int reqInvoiceID);
        void UpdateServiceRequest(string idColumn, int id, string columnName1, string status, string columnName2, DateTime resovedDate);
    }
}
