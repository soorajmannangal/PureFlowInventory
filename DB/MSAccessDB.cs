using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PureFlow
{
    public class MSAccessDB : IDataSource
    {
        private const string INSERT_INTO = "INSERT INTO";
        private const string SELECT = "SELECT";
        private const string VALUES = "VALUES";
        private const string FROM = "FROM";
        private const string WHERE = "WHERE";
        private const string UPDATE = "UPDATE";
        private const string ORDER_BY = "ORDER BY";
        private const string SET = "SET";

        private OleDbConnection con;
        private OleDbCommand cmd;
        private const String DB_NAME = @"\DB\PureFlowDB.accdb";

        public MSAccessDB()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + DB_NAME;
            con = new OleDbConnection(connectionString);
        }

        public List<ComboDto> GetColumnDataById(eTableNames tableName, string columnName, int id)
        {
            List<ComboDto> columnData = new List<ComboDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {eGenericColumnName.ID},{columnName} {FROM} {tableName} {WHERE} {eGenericColumnName.ID}={id} {ORDER_BY} {columnName}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                columnData.Add(new ComboDto(int.Parse(reader[eGenericColumnName.ID.ToString()].ToString()), reader[columnName].ToString()));
            }
            con.Close();
            return columnData;
        }
        public List<ComboDto> GetColumnData(eTableNames tableName, string columnName)
        {
            List<ComboDto> columnData = new List<ComboDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {eGenericColumnName.ID},{columnName} {FROM} {tableName} {ORDER_BY} {columnName}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                columnData.Add(new ComboDto(int.Parse(reader[eGenericColumnName.ID.ToString()].ToString()), reader[columnName].ToString()));
            }
            con.Close();
            return columnData;
        }

        public List<ComboDto> GetColumnDataByFKId(eTableNames tableName, string columnName, string fkIdColumnName, int fkIdValue )
        {
            List<ComboDto> columnData = new List<ComboDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {eGenericColumnName.ID},{columnName} {FROM} {tableName} {WHERE} {fkIdColumnName}={fkIdValue} {ORDER_BY} {columnName}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                columnData.Add(new ComboDto(int.Parse(reader[eGenericColumnName.ID.ToString()].ToString()), reader[columnName].ToString()));
            }
            con.Close();
            return columnData;
        }


        public int GetId(eTableNames tableName, object columnName, object columnValue )
        {
            string val = TypeConvertor(columnValue);
            string colName = columnName.ToString();
            int id = 0;
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {eGenericColumnName.ID} {FROM} {tableName} {WHERE} {colName}={val}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            con.Close();
            return id;
        }

        public ObservableCollection<BrandGridDto> GetAllBrands(string id, string name, string details, string orderBy)
        {
            ObservableCollection<BrandGridDto> allBrands = new ObservableCollection<BrandGridDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{details} {FROM} {eTableNames.Brand} {ORDER_BY} {name}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allBrands.Add(new BrandGridDto()
                {
                    ID = int.Parse(reader[id].ToString()),
                    Name = reader[name].ToString(),
                    Details = reader[details].ToString()
                });              
            }
            con.Close();
            return allBrands;
        }

       

        public CustomerGridDto GetCustomerByPhone(string id, string name, string phone, string address, string email, string phoneNoToMatch)
        {
            CustomerGridDto dto = null;
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{phone},{email},{address} {FROM} {eTableNames.Customer} {WHERE} {phone}={Str(phoneNoToMatch)}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dto = (new CustomerGridDto()
                {
                    ID = int.Parse(reader[id].ToString()),
                    Name = reader[name].ToString(),
                    Phone = reader[phone].ToString(),
                    Address = reader[address].ToString(),
                    Email = reader[email].ToString()
                });
                break;
            }
            con.Close();
            return dto;
        }

        public int GetLastId(eTableNames tableName, string columnName)
        {
            int lastId = 0;
            con.Open();
            cmd = new OleDbCommand($"{SELECT} MAX({columnName}) {FROM} {tableName}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lastId = int.Parse(reader[0].ToString());
                   
            }
            con.Close();
            return lastId;
        }

        public List<CustomerGridDto> GetAllCustomers(string id, string name, string phone, string address, string email, string orderBy)
        {
            List<CustomerGridDto> dtos = new List<CustomerGridDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{phone},{email},{address} {FROM} {eTableNames.Customer} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dtos.Add(new CustomerGridDto()
                {
                    ID = int.Parse(reader[id].ToString()),
                    Name = reader[name].ToString(),
                    Phone = reader[phone].ToString(),
                    Address = reader[address].ToString(),
                    Email = reader[email].ToString()
                });
            }
            con.Close();
            return dtos;
        }

      
        public List<ServiceRequestGridDto> GetServiceRequestList(string id, string CustomerID, string Details, string IsUnderWarranty, string DateOfEntry,
            string Status,
            string BrandID,
            string ModelID,
            string RequestDate,
            string resolvedDate,
            string orderBy            )
        {
            List<ServiceRequestGridDto> dtos = new List<ServiceRequestGridDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{CustomerID},{Details},{IsUnderWarranty},{DateOfEntry},{Status},{BrandID},{ModelID},{RequestDate},{resolvedDate} {FROM} {eTableNames.ServiceRequest} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dtos.Add(new ServiceRequestGridDto()
                {
                    ID = int.Parse(reader[id].ToString()),
                    CustomerID = int.Parse(reader[CustomerID].ToString()),                 
                    Details = reader[Details].ToString(),
                    IsUnderWarranty = bool.Parse( reader[IsUnderWarranty].ToString()),
                    DateOfEntry = DateTime.Parse( reader[DateOfEntry].ToString()),
                    Status = reader[Status].ToString(),
                    BrandID = int.Parse(reader[BrandID].ToString()),
                    ModelID = int.Parse(reader[ModelID].ToString()),
                    RequestDate = DateTime.Parse(reader[RequestDate].ToString()),
                    ResolvedDate = DateTime.Parse(reader[resolvedDate].ToString()),
                      
                });
            }
            con.Close();
            return dtos;
        }

        public List<ServiceRequestGridDto> GetServiceRequestListForCustomerId(string id, string CustomerID, string Details, string IsUnderWarranty, string DateOfEntry,
            string Status,
            string BrandID,
            string ModelID,
            string RequestDate,
            string resolvedDate,
            string orderBy,
            int customerId)
        {
            List<ServiceRequestGridDto> dtos = new List<ServiceRequestGridDto>();
            con.Open();

            string condition = $" {CustomerID} = {customerId} AND {resolvedDate} =  Format(#" + DateTime.MinValue + "#, 'dd/mm/yyyy') ";
            string fieldsToSelect = $"{id},{CustomerID},{Details},{IsUnderWarranty},{DateOfEntry},{Status},{BrandID},{ModelID},{RequestDate},{resolvedDate}";
            cmd = new OleDbCommand($"{SELECT} {fieldsToSelect} {FROM} {eTableNames.ServiceRequest} {WHERE} {condition}  {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dtos.Add(new ServiceRequestGridDto()
                {
                    ID = int.Parse(reader[id].ToString()),
                    CustomerID = int.Parse(reader[CustomerID].ToString()),
                    Details = reader[Details].ToString(),
                    IsUnderWarranty = bool.Parse(reader[IsUnderWarranty].ToString()),
                    DateOfEntry = DateTime.Parse(reader[DateOfEntry].ToString()),
                    Status = reader[Status].ToString(),
                    BrandID = int.Parse(reader[BrandID].ToString()),
                    ModelID = int.Parse(reader[ModelID].ToString()),
                    RequestDate = DateTime.Parse(reader[RequestDate].ToString()),
                    ResolvedDate = DateTime.Parse(reader[resolvedDate].ToString()),
                });
            }
            con.Close();
            return dtos;
        }

        public void UpdateServiceRequest(string idColumn, int id, string columnName1, string status, string columnName2, DateTime resovedDate)
        {
            OleDbCommand cmd = new OleDbCommand();
            con.Open();
            cmd.CommandText = $"UPDATE {eTableNames.ServiceRequest} SET {columnName1} = {Str(status)}, {columnName2} = {Str(resovedDate.ToString())}  {WHERE} {idColumn}= {id}";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void Insert(params object[] p)
        {
            //TableName - column name, value, column name, value
            int len = p.Length;
            if (len < 3)
            {
                throw new InvalidDataException("Invalid format");
            }

            string tableName = p[0].ToString();
            StringBuilder query = new StringBuilder();
            query.Append($"{INSERT_INTO} {tableName}(");
            for (int i = 1; i < len; i += 2)
            {
                string val = p[i].ToString();
                query.Append(val);
                if (i +  2 < len)
                {
                    query.Append(",");
                }
            }
            query.Append($") {VALUES}(");
            for (int i = 2; i < len; i += 2)
            {
                query.Append(TypeConvertor(p[i]));             
                if (i + 1 < len)
                {
                    query.Append(",");
                }
            }
            query.Append(")");

            OleDbCommand cmd = new OleDbCommand();
            con.Open();
            cmd.CommandText = query.ToString();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string TypeConvertor(object p)
        {
            string query;
            Type tp = p.GetType();
            string val = p.ToString();
            if (tp.Equals(typeof(string)) || tp.Equals(typeof(DateTime)))
            {
                query = Str(val);
            }
            else if (tp.Equals(typeof(int)))
            {
                query = val;
            }
            else if (tp.Equals(typeof(double)))
            {
                query = val;
            }
            else if(tp.Equals(typeof(bool)))
            {
                query = val;
            }
            else if (tp.Equals(typeof(decimal)))
            {
                query = val;
            }
            else
            {
                throw new InvalidDataException("Invalid data type" + tp.GetType());
            }

            return query;
        }

        private string Str(string value)
        {
            return "'" + value.Trim() + "'";
        }


        public int GetSingleColumnValueById(eTableNames tableName, int rowId, string columnName)
        {
            int val = 0;
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {columnName} {FROM} {tableName} {WHERE} {eGenericColumnName.ID}={rowId}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                val = int.Parse(reader[columnName].ToString());

            }
            con.Close();
            return val;
        }

        public void UpdateSingleColumn(eTableNames tableName, int rowId, string columnName, object columnValue)
        {
            string val = TypeConvertor(columnValue);
            OleDbCommand cmd = new OleDbCommand();  
            con.Open();
            cmd.CommandText = $"{UPDATE} {tableName} {SET} {columnName} = {val}  {WHERE} {eGenericColumnName.ID}={rowId}";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public ObservableCollection<SpareInventoryDto> GetAllSpares(string id, string name, string details, string quantity, string orderBy)
        {
            ObservableCollection<SpareInventoryDto> results = new ObservableCollection<SpareInventoryDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{quantity},{details} {FROM} {eTableNames.SpareInventory} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                string resName = reader[name].ToString();
                string resDetails = reader[details].ToString();
                int resQty = int.Parse(reader[quantity].ToString());
                results.Add(
                    new SpareInventoryDto()
                    {
                        ID = resId,
                        Name = resName,
                        Details = resDetails,
                        Quantity = resQty,
                    });                                       
            }
            con.Close();
            return results;
        }

        public List<SpareInventoryDto> GetAllSparesWithStock(string id, string name, string details, string quantity, string orderBy)
        {
            List<SpareInventoryDto> results = new List<SpareInventoryDto>();
            con.Open();
            string condition = $" {WHERE} {quantity} > 0 ";
            cmd = new OleDbCommand($"{SELECT} {id},{name},{quantity},{details} {FROM} {eTableNames.SpareInventory} {condition} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                string resName = reader[name].ToString();
                string resDetails = reader[details].ToString();
                int resQty = int.Parse(reader[quantity].ToString());
                results.Add(
                    new SpareInventoryDto()
                    {
                        ID = resId,
                        Name = resName,
                        Details = resDetails,
                        Quantity = resQty,
                    });
            }
            con.Close();
            return results;
        }

        public List<InvoiceItemsGridDto> GetInvoiceItems(string id, string invoiceID, string spareInventoryID, string qty, string workTypeID, string amount, int reqInvoiceID)
        {
            List<InvoiceItemsGridDto> results = new List<InvoiceItemsGridDto>();
            con.Open();

            cmd = new OleDbCommand($"{SELECT} {id},{invoiceID},{spareInventoryID},{qty},{workTypeID},{amount} {FROM} {eTableNames.Invoice} {WHERE} {invoiceID}={reqInvoiceID}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                int resinvoiceID = int.Parse(reader[invoiceID].ToString());
                int resspareInventoryID = int.Parse(reader[spareInventoryID].ToString());
                int resqty = int.Parse(reader[qty].ToString());
                int resworkTypeID = int.Parse(reader[workTypeID].ToString());
                decimal resamount = decimal.Parse(reader[amount].ToString());
                results.Add(
                    new InvoiceItemsGridDto()
                    {
                        ID = resId,
                        InvoiceID = resinvoiceID,
                        SpareInventoryID = resspareInventoryID,
                        Qty = resqty,
                        WorkTypeID = resworkTypeID,
                        Amount = resamount,                     
                    });
            }
            con.Close();
            return results;
        }

        public ObservableCollection<InvoiceGridDto> GetAllInvoices(string id, string customerID, string invoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string note, string orderBy)
        {
            ObservableCollection<InvoiceGridDto> results = new ObservableCollection<InvoiceGridDto>();
            con.Open();
            
            cmd = new OleDbCommand($"{SELECT} {id},{customerID},{invoiceDate},{serviceRequestID},{serviceManID},{nextServiceDueDate},{totalAmount},{note} {FROM} {eTableNames.Invoice} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                DateTime resinvoiceDate = DateTime.Parse(reader[invoiceDate].ToString());
                DateTime resnextServiceDueDate = DateTime.Parse(reader[nextServiceDueDate].ToString());
                int rescustomerID = int.Parse(reader[customerID].ToString());
                int resserviceRequestID = int.Parse(reader[serviceRequestID].ToString());
                int resserviceManID = int.Parse(reader[serviceManID].ToString());
                decimal restotalAmount = decimal.Parse(reader[totalAmount].ToString());
                string resnote = reader[note].ToString();
                results.Add(
                    new InvoiceGridDto()
                    {
                        ID = resId,
                        InvoiceDate = resinvoiceDate,
                        CustomerID = rescustomerID,
                        ServiceRequestID = resserviceRequestID,
                        ServiceManID = resserviceManID,
                        NextServiceDueDate = resnextServiceDueDate,
                        TotalAmount = restotalAmount,
                        Notes = resnote,
                    });
            }
            con.Close();
            return results;
        }

        public ObservableCollection<InvoiceGridDto> GetInvoicesForAPeriod(DateTime fromDate, DateTime toDate, string id, string customerID, string invoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string note, string orderBy)
        {
            ObservableCollection<InvoiceGridDto> results = new ObservableCollection<InvoiceGridDto>();
            con.Open();
            string condition = $" {nextServiceDueDate} Between  Format(#" + fromDate + "#, 'dd/mm/yyyy') And   Format(#" + toDate + "#, 'dd/mm/yyyy') ";
            cmd = new OleDbCommand($"{SELECT} {id},{customerID},{invoiceDate},{serviceRequestID},{serviceManID},{nextServiceDueDate},{totalAmount},{note} {FROM} {eTableNames.Invoice} {WHERE} {condition} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                DateTime resinvoiceDate = DateTime.Parse(reader[invoiceDate].ToString());
                DateTime resnextServiceDueDate = DateTime.Parse(reader[nextServiceDueDate].ToString());
                int rescustomerID = int.Parse(reader[customerID].ToString());
                int resserviceRequestID = int.Parse(reader[serviceRequestID].ToString());
                int resserviceManID = int.Parse(reader[serviceManID].ToString());
                decimal restotalAmount = decimal.Parse(reader[totalAmount].ToString());
                string resnote = reader[note].ToString();
                results.Add(
                    new InvoiceGridDto()
                    {
                        ID = resId,
                        InvoiceDate = resinvoiceDate,
                        CustomerID = rescustomerID,
                        ServiceRequestID = resserviceRequestID,
                        ServiceManID = resserviceManID,
                        NextServiceDueDate = resnextServiceDueDate,
                        TotalAmount = restotalAmount,
                        Notes = resnote,
                    });
            }
            con.Close();
            return results;
        }


        public List<InventoryTransactionDto> GetInventoryTransactionData(string id, string spareInventoryID, string qty, string userID, string transactionDate, string orderBy)
        {
            List<InventoryTransactionDto> results = new List<InventoryTransactionDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{spareInventoryID},{qty},{userID},{transactionDate} {FROM} {eTableNames.InventoryTransaction} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(
                    new InventoryTransactionDto()
                    {
                        ID = int.Parse(reader[id].ToString()),
                        SpareInventoryID = int.Parse(reader[spareInventoryID].ToString()),
                        Qty = int.Parse(reader[qty].ToString()),
                        UserID = int.Parse(reader[userID].ToString()),
                        TransactionDate = DateTime.Parse(reader[transactionDate].ToString())
                    });
            }
            con.Close();
            return results;
        }
    }
}
