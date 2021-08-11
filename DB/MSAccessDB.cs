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
        private const String DB_NAME = @"\PureFlowDB.accdb";

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


        public bool GetBoolColWithID(eTableNames tableName, int id, string columnName )
        {
          
            bool status = false;
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {columnName} {FROM} {tableName} {WHERE} {eGenericColumnName.ID}={id}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                status = reader.GetBoolean(0);
            }
            con.Close();
            return status;
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

        public ObservableCollection<BrandDto> GetAllBrands(string id, string name, string details, string orderBy)
        {
            ObservableCollection<BrandDto> allBrands = new ObservableCollection<BrandDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{details} {FROM} {eTableNames.BrandsTable} {ORDER_BY} {name}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allBrands.Add(new BrandDto()
                {
                    ID = int.Parse(reader[id].ToString()),
                    Name = reader[name].ToString(),
                    Details = reader[details].ToString()
                });              
            }
            con.Close();
            return allBrands;
        }

        public ObservableCollection<ModelDto> GetAllModels()
        {
            ObservableCollection<ModelDto> allBrands = new ObservableCollection<ModelDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} BT.{eBrandsTable.Name} as Brand, MT.{eModelsTable.Name} as Model, MT.{eModelsTable.Details} as Details {FROM} {eTableNames.ModelsTable} as MT inner join {eTableNames.BrandsTable} as BT on MT.{eModelsTable.BrandID} = BT.{eBrandsTable.ID}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            int index = 1; 
            while (reader.Read())
            {
                allBrands.Add(new ModelDto()
                {
                    ID = index++,
                    BrandName = reader["Brand"].ToString(),
                    ModelName = reader["Model"].ToString(),
                    Details = reader["Details"].ToString()
                });
            }
            con.Close();
            return allBrands;
        }



        public CustomerDto GetCustomerByPhone(string id, string name, string phone, string address, string email, string phoneNoToMatch)
        {
            CustomerDto dto = null;
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{phone},{email},{address} {FROM} {eTableNames.CustomerTable} {WHERE} {phone}={Str(phoneNoToMatch)}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dto = (new CustomerDto()
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

        public List<CustomerDto> GetAllCustomers(string id, string name, string phone, string address, string email, string orderBy)
        {
            List<CustomerDto> dtos = new List<CustomerDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{phone},{email},{address} {FROM} {eTableNames.CustomerTable} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dtos.Add(new CustomerDto()
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

      
        public List<ServiceRequestDto> GetServiceRequestList(string id, string CustomerID, string Details, string IsUnderWarranty, string DateOfEntry,
            string Status,
            string BrandID,
            string ModelID,
            string RequestDate,
            string resolvedDate,
            string orderBy            )
        {
            List<ServiceRequestDto> dtos = new List<ServiceRequestDto>();
            con.Open();
            string selection = $"SRT.{id} as {id},SRT.{CustomerID} as {CustomerID},SRT.{Details} as {Details},SRT.{IsUnderWarranty} as {IsUnderWarranty},SRT.{DateOfEntry} as {DateOfEntry},SRT.{Status} as {Status},SRT.{BrandID} as {BrandID},SRT.{ModelID} as {ModelID},SRT.{RequestDate} as {RequestDate},SRT.{resolvedDate} as {resolvedDate}" +
                $", CT.{eCustomerTable.Name} as CustomerName, CT.{eCustomerTable.Phone} as Phone " +
                $", BT.{eBrandsTable.Name} as BrandName" +
                $", MT.{eModelsTable.Name} as ModelName ";

            string source = $" (({eTableNames.ServiceRequestTable} as SRT inner join {eTableNames.CustomerTable} as CT on SRT.{eServiceRequestTable.CustomerID} = CT.{eCustomerTable.ID.ToString()} ) " +
                $"inner join {eTableNames.BrandsTable} as BT on BT.{eBrandsTable.ID} = SRT.{eServiceRequestTable.BrandID})" +
                $" inner join {eTableNames.ModelsTable} as MT on MT.{eModelsTable.ID} = SRT.{eServiceRequestTable.ModelID} ";
       
            cmd = new OleDbCommand($"{SELECT} {selection} {FROM} {source}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            int index = 1;
            while (reader.Read())
            {
                dtos.Add(new ServiceRequestDto()
                {
                    Ind = index++,
                    ID = int.Parse(reader[id].ToString()),
                    CustomerID = int.Parse(reader[CustomerID].ToString()),
                    CustomerName = reader["CustomerName"].ToString(), //
                    CustomerPhone = reader["Phone"].ToString(), //
                    BrandName = reader["BrandName"].ToString(), //
                    ModelName = reader["ModelName"].ToString(), //
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

        public List<ServiceRequestDto> GetServiceRequestListForCustomerId(string id, string CustomerID, string Details, string IsUnderWarranty, string DateOfEntry,
            string Status,
            string BrandID,
            string ModelID,
            string RequestDate,
            string resolvedDate,
            string orderBy,
            int customerId)
        {
            List<ServiceRequestDto> dtos = new List<ServiceRequestDto>();
            con.Open();

            string condition = $" {CustomerID} = {customerId} AND {resolvedDate} =  Format(#" + DateTime.MinValue + "#, 'dd/mm/yyyy') ";
            string fieldsToSelect = $"{id},{CustomerID},{Details},{IsUnderWarranty},{DateOfEntry},{Status},{BrandID},{ModelID},{RequestDate},{resolvedDate}";
            cmd = new OleDbCommand($"{SELECT} {fieldsToSelect} {FROM} {eTableNames.ServiceRequestTable} {WHERE} {condition}  {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dtos.Add(new ServiceRequestDto()
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
            cmd.CommandText = $"UPDATE {eTableNames.ServiceRequestTable} SET {columnName1} = {Str(status)}, {columnName2} = {Str(resovedDate.ToString())}  {WHERE} {idColumn}= {id}";
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

        public ObservableCollection<InventoryDto> GetAllSpares(string id, string name, string details, string quantity, string orderBy)
        {
            ObservableCollection<InventoryDto> results = new ObservableCollection<InventoryDto>();
            con.Open();
            cmd = new OleDbCommand($"{SELECT} {id},{name},{quantity},{details} {FROM} {eTableNames.InventoryTable} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                string resName = reader[name].ToString();
                string resDetails = reader[details].ToString();
                int resQty = int.Parse(reader[quantity].ToString());
                results.Add(
                    new InventoryDto()
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

        public List<InventoryDto> GetAllSparesWithStock(string id, string name, string details, string quantity, string orderBy)
        {
            List<InventoryDto> results = new List<InventoryDto>();
            con.Open();
            string condition = $" {WHERE} {quantity} > 0 ";
            cmd = new OleDbCommand($"{SELECT} {id},{name},{quantity},{details} {FROM} {eTableNames.InventoryTable} {condition} {ORDER_BY} {orderBy}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                string resName = reader[name].ToString();
                string resDetails = reader[details].ToString();
                int resQty = int.Parse(reader[quantity].ToString());
                results.Add(
                    new InventoryDto()
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

        public List<InvoiceItemsDto> GetInvoiceItems(string id, string invoiceID, string spareInventoryID, string qty, string workTypeID, string amount, int reqInvoiceID)
        {
            List<InvoiceItemsDto> results = new List<InvoiceItemsDto>();
            con.Open();

            cmd = new OleDbCommand($"{SELECT} {id},{invoiceID},{spareInventoryID},{qty},{workTypeID},{amount} {FROM} {eTableNames.InvoiceTable} {WHERE} {invoiceID}={reqInvoiceID}", con);
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
                    new InvoiceItemsDto()
                    {
                        ID = resId,
                        InvoiceID = resinvoiceID,
                        InventoryID = resspareInventoryID,
                        Qty = resqty,
                        WorkTypeID = resworkTypeID,
                        Amount = resamount,                     
                    });
            }
            con.Close();
            return results;
        }

        public ObservableCollection<InvoiceDto> GetAllInvoices(string id, string customerID, string invoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string notes, string orderBy)
        {
            ObservableCollection<InvoiceDto> results = new ObservableCollection<InvoiceDto>();
            con.Open();
            string selection = $"IT.{id} as {id},IT.{customerID} as {customerID},IT.{invoiceDate} as {invoiceDate},IT.{serviceRequestID} as {serviceRequestID},IT.{serviceManID} as {serviceManID},IT.{nextServiceDueDate} as {nextServiceDueDate},IT.{totalAmount} as {totalAmount},IT.{notes} as {notes}" +
                $", CT.{eCustomerTable.Name} as CustomerName, CT.{eCustomerTable.Phone} as CustomerPhone," +
                $"TT.{eTechnicianTable.Name} as TechnicianName, UT.{eUserTable.Name} as UserName";
            string source = $"(( {eTableNames.InvoiceTable} as IT inner join {eTableNames.CustomerTable} as CT on IT.{eInvoiceTable.CustomerID} = CT.{eCustomerTable.ID} )" +
                $"inner join {eTableNames.TechnicianTable} as TT on IT.{eInvoiceTable.TechnicianID} = TT.{eTechnicianTable.ID})" +
                $"inner join {eTableNames.UserTable} as UT on UT.{eUserTable.ID} = IT.{eInvoiceTable.UserID}";
            cmd = new OleDbCommand($"{SELECT} {selection} {FROM}  {source}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            int index = 1;
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                DateTime resinvoiceDate = DateTime.Parse(reader[invoiceDate].ToString());
                DateTime resnextServiceDueDate = DateTime.Parse(reader[nextServiceDueDate].ToString());
                int rescustomerID = int.Parse(reader[customerID].ToString());
                int resserviceRequestID = int.Parse(reader[serviceRequestID].ToString());
                int resserviceManID = int.Parse(reader[serviceManID].ToString());
                decimal restotalAmount = decimal.Parse(reader[totalAmount].ToString());
                string resnote = reader[notes].ToString();
                results.Add(
                    new InvoiceDto()
                    {
                        Ind = index++,
                        ID = resId,
                        InvoiceDate = resinvoiceDate,
                        CustomerID = rescustomerID,
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerPhone = reader["CustomerPhone"].ToString(),
                        TechnicianName = reader["TechnicianName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        ServiceRequestID = resserviceRequestID,
                        TechnicianID = resserviceManID,
                        NextServiceDueDate = resnextServiceDueDate,
                        TotalAmount = restotalAmount,
                        Notes = resnote,
                    });
            }
            con.Close();
            return results;
        }

        public ObservableCollection<InvoiceDto> GetServiceDueForAPeriod(DateTime fromDate, DateTime toDate, string id, string customerID, string invoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string notes, string orderBy)
        {
            ObservableCollection<InvoiceDto> results = new ObservableCollection<InvoiceDto>();
            con.Open();
            string selection = $"IT.{id} as {id},IT.{customerID} as {customerID},IT.{invoiceDate} as {invoiceDate},IT.{serviceRequestID} as {serviceRequestID},IT.{serviceManID} as {serviceManID},IT.{nextServiceDueDate} as {nextServiceDueDate},IT.{totalAmount} as {totalAmount},IT.{notes} as {notes}" +
                $", CT.{eCustomerTable.Name} as CustomerName, CT.{eCustomerTable.Phone} as CustomerPhone," +
                $"TT.{eTechnicianTable.Name} as TechnicianName, UT.{eUserTable.Name} as UserName";
            string source = $"(( {eTableNames.InvoiceTable} as IT inner join {eTableNames.CustomerTable} as CT on IT.{eInvoiceTable.CustomerID} = CT.{eCustomerTable.ID} )" +
                $"inner join {eTableNames.TechnicianTable} as TT on IT.{eInvoiceTable.TechnicianID} = TT.{eTechnicianTable.ID})" +
                $"inner join {eTableNames.UserTable} as UT on UT.{eUserTable.ID} = IT.{eInvoiceTable.UserID}";

            string condition = $" IT.{eInvoiceTable.NextServiceDueDate} Between  Format(#" + fromDate + "#, 'dd/mm/yyyy') And   Format(#" + toDate + "#, 'dd/mm/yyyy') ";

            cmd = new OleDbCommand($"{SELECT} {selection} {FROM}  {source} {WHERE} {condition}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            int index = 1;
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                DateTime resinvoiceDate = DateTime.Parse(reader[invoiceDate].ToString());
                DateTime resnextServiceDueDate = DateTime.Parse(reader[nextServiceDueDate].ToString());
                int rescustomerID = int.Parse(reader[customerID].ToString());
                int resserviceRequestID = int.Parse(reader[serviceRequestID].ToString());
                int resserviceManID = int.Parse(reader[serviceManID].ToString());
                decimal restotalAmount = decimal.Parse(reader[totalAmount].ToString());
                string resnote = reader[notes].ToString();
                results.Add(
                    new InvoiceDto()
                    {
                        Ind = index++,
                        ID = resId,
                        InvoiceDate = resinvoiceDate,
                        CustomerID = rescustomerID,
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerPhone = reader["CustomerPhone"].ToString(),
                        TechnicianName = reader["TechnicianName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        ServiceRequestID = resserviceRequestID,
                        TechnicianID = resserviceManID,
                        NextServiceDueDate = resnextServiceDueDate,
                        TotalAmount = restotalAmount,
                        Notes = resnote,
                    });
            }
            con.Close();
            return results;
        }


        public ObservableCollection<InvoiceDto> GetInvoicesForAPeriod(DateTime fromDate, DateTime toDate, string id, string customerID, string invoiceDate, string serviceRequestID, string serviceManID, string nextServiceDueDate, string totalAmount, string notes, string orderBy)
        {
            ObservableCollection<InvoiceDto> results = new ObservableCollection<InvoiceDto>();
            con.Open();
            string selection = $"IT.{id} as {id},IT.{customerID} as {customerID},IT.{invoiceDate} as {invoiceDate},IT.{serviceRequestID} as {serviceRequestID},IT.{serviceManID} as {serviceManID},IT.{nextServiceDueDate} as {nextServiceDueDate},IT.{totalAmount} as {totalAmount},IT.{notes} as {notes}" +
                $", CT.{eCustomerTable.Name} as CustomerName, CT.{eCustomerTable.Phone} as CustomerPhone," +
                $"TT.{eTechnicianTable.Name} as TechnicianName, UT.{eUserTable.Name} as UserName";
            string source = $"(( {eTableNames.InvoiceTable} as IT inner join {eTableNames.CustomerTable} as CT on IT.{eInvoiceTable.CustomerID} = CT.{eCustomerTable.ID} )" +
                $"inner join {eTableNames.TechnicianTable} as TT on IT.{eInvoiceTable.TechnicianID} = TT.{eTechnicianTable.ID})" +
                $"inner join {eTableNames.UserTable} as UT on UT.{eUserTable.ID} = IT.{eInvoiceTable.UserID}";

            string condition = $" IT.{eInvoiceTable.InvoiceDate} Between  Format(#" + fromDate + "#, 'dd/mm/yyyy') And   Format(#" + toDate + "#, 'dd/mm/yyyy') ";

            cmd = new OleDbCommand($"{SELECT} {selection} {FROM}  {source} {WHERE} {condition}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            int index = 1;
            while (reader.Read())
            {
                int resId = int.Parse(reader[id].ToString());
                DateTime resinvoiceDate = DateTime.Parse(reader[invoiceDate].ToString());
                DateTime resnextServiceDueDate = DateTime.Parse(reader[nextServiceDueDate].ToString());
                int rescustomerID = int.Parse(reader[customerID].ToString());
                int resserviceRequestID = int.Parse(reader[serviceRequestID].ToString());
                int resserviceManID = int.Parse(reader[serviceManID].ToString());
                decimal restotalAmount = decimal.Parse(reader[totalAmount].ToString());
                string resnote = reader[notes].ToString();
                results.Add(
                    new InvoiceDto()
                    {
                        Ind = index++,
                        ID = resId,
                        InvoiceDate = resinvoiceDate,
                        CustomerID = rescustomerID,
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerPhone = reader["CustomerPhone"].ToString(),
                        TechnicianName = reader["TechnicianName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        ServiceRequestID = resserviceRequestID,
                        TechnicianID = resserviceManID,
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
            string selection = $" IT.{id} as {id}, IT.{spareInventoryID} as {spareInventoryID},IT.{qty} as {qty}, IT.{userID} as {userID},  SI.Name as SpareName, US.Name as UserName, FORMAT(IT.{transactionDate}, 'Short Date') as {transactionDate} ";
            string source = $" (({eTableNames.InventoryTransactionTable} as IT inner join {eTableNames.InventoryTable} as SI on IT.SpareInventoryID = SI.ID ) inner join {eTableNames.UserTable} as US on IT.{userID} = US.ID) ";
            cmd = new OleDbCommand($"{SELECT} {selection} {FROM} {source}", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            int ind = 1;
            while (reader.Read())
            {
                results.Add(
                    new InventoryTransactionDto()
                    {
                        Ind = ind++,
                        ID = int.Parse(reader[id].ToString()),
                        SpareInventoryID = int.Parse(reader[spareInventoryID].ToString()),
                        SpareName = reader["SpareName"].ToString(),
                        Qty = int.Parse(reader[qty].ToString()),
                        UserID = int.Parse(reader[userID].ToString()),
                        UserName = reader["UserName"].ToString(),
                        TransactionDate = DateTime.Parse(reader[transactionDate].ToString())
                    }) ;
            }
            con.Close();
            return results;
        }
    }
}
