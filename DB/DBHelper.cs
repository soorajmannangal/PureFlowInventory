using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace PureFlow
{ 
    public class DBHelper
    {        
        private OleDbConnection con;
        private OleDbCommand cmd;
   
        private static DBHelper instance;
        public readonly IDataSource DataSource;

        public static DBHelper GetInstance()
        {
            return instance ?? (instance = new DBHelper());
        }

        private DBHelper()
        {
            DataSource = new MSAccessDB();
        }

      
        public int GetBranchID(string branchName, int customerId)
        {
            int id = 0;
            con.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("select ID from Branch where name='" + branchName + "' and customerId="+customerId+" order by id", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            con.Close();
            return id;
        }

   
        public DataTable GetTableData(string commandText)
        {
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = commandText;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetBranchDetails()
        {
            return GetTableData(@"SELECT c.name as CustomerName, b.name as Branch FROM (branch as b left join customer as c on b.customerid= c.id) order by c.name");
        }

        public DataTable GetProductDetails()
        {
            return GetTableData(@"SELECT c.name as CategoryName, p.name as ProductName, p.Description as Description FROM (product as p left join Category as c on p.categoryid= c.id)");
        }

        public DataTable GetCustomerName()
        {
          return GetTableData("SELECT name as CustomerName FROM Customer order by name");
        }

        public bool DeleteTransactionRow(int id)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                using (var myCommand = con.CreateCommand())
                {
                    var idParam = new OleDbParameter("@id", id);
                    myCommand.CommandText = "DELETE FROM GenTrans WHERE (ID) = @id";
                    myCommand.Parameters.Add(idParam);
                    con.Open();
                    myCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public DataTable GetCategoryrName()
        {
            return GetTableData("SELECT name as CategoryName FROM Category order by name");
        }

        public List<PageIndex> GetTransactionPages(int rowsPerPage, string customerName, string branchName, string productCategory, string productName, DateTime fromDate, DateTime toDate)
        {
            DataTable dataTable = GetTransactionsIdOnly( customerName,  branchName,  productCategory,  productName, fromDate, toDate);
            int rowCount = dataTable.Rows.Count;
            int index = 0;
            List<PageIndex> pageIndices = new List<PageIndex>();
            while(index < rowCount ) 
            {
                PageIndex pi = new PageIndex();
                pi.start = dataTable.Rows[index].Field<int>("ID");                  
                index += rowsPerPage; 
                if(index >= rowCount)
                {
                    pi.end = dataTable.Rows[rowCount-1].Field<int>("ID");
                }
                else
                {
                    pi.end = dataTable.Rows[index -1].Field<int>("ID");
                }
                pageIndices.Add(pi);               
            }
            return pageIndices;
        }

        public DataTable GetTransactions(string customerName, string branchName, string productCategory, string productName, int startId, int endId, DateTime fromDate, DateTime toDate)
        {
            string condition = GetBaseCondition(ref customerName, ref branchName, ref productCategory, ref productName, ref fromDate, ref toDate);
            if(!(startId == 0 && endId == 0))
            {
                if (string.IsNullOrEmpty(condition))
                {
                    condition += " Where ";
                }
                else
                {
                    condition += " and ";
                }

                if (startId == endId)
                {
                    condition += " g.id = " + startId;
                }
                else
                {
                    condition += " g.id >= " + endId + " and g.id <= " + startId ;
                }
            }

            string query = "SElECT g.id as ID, FORMAT(g.transDate, 'Short Date') as TransactionDate, CU.NAME as Customer, b.name as BranchName, ca.name as ProductCategory, p.name as ProductName,g.bookcount as BookCount, g.leafcount as LeafCount, g.amount as Amount, g.details as Details FROM ((((GenTrans as G inner join Customer as CU on G.CUSTOMERID = CU.ID) inner join branch as b on G.branchId = b.id) inner join category as ca on ca.id = g.categoryid) inner join product as p on g.productid=p.id) " + condition + " ORDER BY g.id desc ";
            return GetTableData(query);
        }

        public DataTable GetTransactionsIdOnly(string customerName, string branchName, string productCategory, string productName, DateTime fromDate, DateTime toDate)
        {
            string condition = GetBaseCondition(ref customerName, ref branchName, ref productCategory, ref productName, ref fromDate, ref toDate);
            string query = "SElECT g.id as ID FROM ((((GenTrans as G inner join Customer as CU on G.CUSTOMERID = CU.ID) inner join branch as b on G.branchId = b.id) inner join category as ca on ca.id = g.categoryid) inner join product as p on g.productid=p.id) " + condition + "  ORDER BY g.id desc ";
            return GetTableData(query);
        }

        private static string GetBaseCondition(ref string customerName, ref string branchName, ref string productCategory, ref string productName, ref DateTime fromDate,ref DateTime toDate)
        {
            if (customerName == null) customerName = "All";
            if (branchName == null) branchName = "All";
            if (productCategory == null) productCategory = "All";
            if (productName == null) productName = "All";
            string condition = "";
            if (!(customerName.Equals("All") && productCategory.Equals("All")))
            {
                condition += " Where ";
            }
            string extn = "";
            if (!customerName.Equals("All"))
            {
                condition += " cu.name = '" + customerName + "' ";
                if (!branchName.Equals("All"))
                {
                   condition += "and b.name = '" + branchName + "' ";
                }
                extn = " and ";
            }

            if (!productCategory.Equals("All"))
            {
                condition += extn + " ca.name = '" + productCategory + "' ";

                if (!productName.Equals("All"))
                {
                   condition += " and p.name = '" + productName+"'";
                }
            }

            if(string.IsNullOrEmpty(condition))
            {
                condition += " Where ";
            }
            else
            {
                condition += " and ";
            }
            condition += " TransDate Between  Format(#" + fromDate + "#, 'dd/mm/yyyy') And   Format(#" + toDate + "#, 'dd/mm/yyyy') ";

            return condition;
        }

        public List<string> GetCustomerNames()
        {
            List<string> customerName = new List<string>();
            con.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("select Name from Customer order by name", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customerName.Add(reader["Name"].ToString());              
            }
            con.Close();
            return customerName;
        }

        //public List<string> GetBranchNames(string customerName)
        //{
        //    int customerId = GetCustomerID(customerName);
        //    List<string> branchNames = new List<string>();
        //    con.Open();            
        //    cmd = new OleDbCommand("select Name from Branch where customerid="+customerId +" order by name", con);
        //    OleDbDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        branchNames.Add(reader["Name"].ToString());
        //    }
        //    con.Close();
        //    return branchNames;
        //}

        public List<string> GetCategoryNames()
        {
            List<string> productCategory = new List<string>();
            con.Open();
            cmd = new OleDbCommand("select Name from Category order by name", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productCategory.Add(reader["Name"].ToString());
            }
            con.Close();
            return productCategory;
        }

        //public List<string> GetProductNames(string categoryName)
        //{
        //    int categoryId = GetCategoryID(categoryName);
        //    List<string> productNames = new List<string>();
        //    con.Open();
        //    cmd = new OleDbCommand("select Name from Product where categoryId=" + categoryId +" order by name", con);
        //    OleDbDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        productNames.Add(reader["Name"].ToString());
        //    }
        //    con.Close();
        //    return productNames;
        //}

        //public string GetProductDescription(string productName)
        //{
        //    int productId = GetProductID(productName);
        //    string productDescription = "";
        //    con.Open();
        //    cmd = new OleDbCommand("select description from Product where id=" + productId, con);
        //    OleDbDataReader reader = cmd.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        productDescription = (reader["description"].ToString());
        //    }
        //    con.Close();
        //    return productDescription;
        //}

        public void NewTransaction(int branchId, int customerId, int categoryId, int productId, DateTime transDate, int leafCount, int bookCount, decimal amount, string details )
        {
            OleDbCommand cmd = new OleDbCommand();
            con.Open();
            cmd.CommandText = "Insert into GenTrans(BranchID, CustomerId, categoryId, ProductId,TransDate,leafCount,bookCount,amount, details) Values("+branchId+","+customerId+","+categoryId+","+productId+",'"+transDate+"',"+leafCount+","+bookCount+","+amount+",'"+details+"')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateTransaction(int transId, int branchId, int customerId, int categoryId, int productId, DateTime transDate, int leafCount, int bookCount, decimal amount, string details)
        {
            OleDbCommand cmd = new OleDbCommand();
            con.Open();
            cmd.CommandText = "update GenTrans set BranchID = "+branchId+", CustomerId="+customerId+", categoryId="+categoryId+", ProductId="+productId+",TransDate='"+transDate+"',leafCount="+leafCount+",bookCount="+bookCount+",amount="+amount+", details='"+details+"' where id="+transId;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool CheckDBVersion(int requiredVersion)
        {
            int version = 0;
            con.Open();
            cmd = new OleDbCommand("select * from versioninfo", con);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                version = int.Parse( reader["MainVersion"].ToString());
            }
            con.Close();
            if (version == requiredVersion)
                return true;
            return false;
        }
    }

    public class PageIndex
    {
        public int start;
        public int end;
    }
}
