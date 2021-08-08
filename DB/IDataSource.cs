using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public enum eTableNames
    {
        BrandsTable,
        CustomerTable,
        InventoryTable,
        InventoryTransactionTable,
        InvoiceItemsTable,
        InvoiceTable,
        ModelsTable,
        ServiceReminderTable,
        ServiceRequestTable,
        TechnicianTable,
        UserTable,
        WorkTypeTable
    }

    public enum eWorkTypeTable
    {
        ID,
        Name
    }

    public enum eUserTable
    {
        ID,
        Name,
        Phone,
        Password,
        IsAdmin,
        IsActive
    }

    public enum eTechnicianTable
    {
        ID,
        Name,
        Phone,
        Details,
        IsActive
    }

    public enum eServiceRequestTable
    {
        ID,
        CustomerID,
        Details,
        IsUnderWarranty,
        DateOfEntry,
        Status,
        BrandID,
        ModelID,
        RequestDate,
        ResovedDate
    }
    public enum eServiceReminderTable
    {
        ID,
        CustomerID,
        BrandID,
        ModelID,
        InvoiceID,
        ExpiaryDate
    }

    public enum eModelsTable
    {
        ID,
        BrandID,
        Name,
        Details
    }

    public enum eInvoiceTable
    {
        ID,
        InvoiceDate,
        CustomerID,
        ServiceRequestID,
        TechnicianID,
        NextServiceDueDate,
        TotalAmount,
        Notes,
        UserID
    }

    public enum eInvoiceItemsTable
    {
        ID,
        InvoiceID,
        SpareInventoryID,
        Qty,
        WorkTypeID,
        Amount
    }

    public enum eInventoryTransactionTable
    {
        ID,
        SpareInventoryID,
        Qty,
        UserID,
        TransactionDate
    }

    public enum eInventoryTable
    {
        ID,
        Name,
        Quantity,
        Details
    }

    public enum eCustomerTable
    {
        ID,
        Name,
        Phone,
        Address,
        Email
    }

    public enum eBrandsTable
    {
        ID,
        Name,
        Details
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
        bool GetBoolColWithID(eTableNames tableName, int id, string columnName);
        void UpdateSingleColumn(eTableNames tableNames, int rowId, string columnName, object columnValue);
        int GetSingleColumnValueById(eTableNames tableName, int rowId, string columnName);
        ObservableCollection<BrandDto> GetAllBrands(string id, string name, string details, string orderBy);
        ObservableCollection<ModelDto> GetAllModels();
        ObservableCollection<InventoryDto> GetAllSpares(string id, string name, string details, string quantity, string orderBy);
        List<InventoryDto> GetAllSparesWithStock(string id, string name, string details, string quantity, string orderBy);
        List<ServiceRequestDto> GetServiceRequestList(string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string orderBy);
        List<InventoryTransactionDto> GetInventoryTransactionData(string id, string spareInventoryID, string qty, string userID, string transactionDate, string orderBy);
        List<CustomerDto> GetAllCustomers(string id, string name, string phone, string address, string email, string orderBy);
        CustomerDto GetCustomerByPhone(string id, string name, string phone, string address, string email, string phoneNoToMatch);
        List<ServiceRequestDto> GetServiceRequestListForCustomerId(string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string orderBy, int customerId);
        ObservableCollection<InvoiceDto> GetAllInvoices(string id, string customerID, string nvoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string note, string orderBy);
        ObservableCollection<InvoiceDto> GetInvoicesForAPeriod(DateTime fromDate, DateTime toDate, string id, string customerID, string nvoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string note, string orderBy);
        ObservableCollection<InvoiceDto> GetServiceDueForAPeriod(DateTime fromDate, DateTime toDate, string id, string customerID, string invoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string notes, string orderBy);


        List<InvoiceItemsDto> GetInvoiceItems(string id, string invoiceID, string spareInventoryID, string qty, string workTypeID, string amount, int reqInvoiceID);
        void UpdateServiceRequest(string idColumn, int id, string columnName1, string status, string columnName2, DateTime resovedDate);
    }
}
