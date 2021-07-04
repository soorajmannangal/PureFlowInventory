using System;
using System.Collections.Generic;
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

        private OleDbConnection con;
        private OleDbCommand cmd;
        private const String DB_NAME = @"\DB\PureFlowDB.accdb";

        public MSAccessDB()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + DB_NAME;
            con = new OleDbConnection(connectionString);
        }

        public int GetId(eTableNames tableName, object columnName, object columnValue )
        {
            string val = TypeConvertor(columnValue);
            string colName = columnName.ToString();
            int id = 0;
            con.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand($"{SELECT} {eGenericColumnName.ID} {FROM} {tableName} {WHERE} {colName}={val}", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            con.Close();
            return id;
        }

        public void Insert(params object[] p)
        {
            //TableName - column name, value, column name, value
            int len = p.Length;

            if (len < 3) throw new InvalidDataException("Invalid format");

            string tableName = p[0].ToString();
            StringBuilder query = new StringBuilder();
            query.Append($"{INSERT_INTO} {tableName}(");
            for (int i = 1; i < len; i += 2)
            {
                string val = p[i].ToString();
                query.Append(val);
                if (i + i + 1 < len)
                {
                    query.Append(",");
                }
            }
            query.Append($") {VALUES}(");
            for (int i = 2; i < len; i += 2)
            {
                Type tp = p[i].GetType();
                string val = p[i].ToString();

                if (tp.Equals(typeof(string)) || tp.Equals(typeof(DateTime)))
                {
                    query.Append(Str(val));
                }
                else if (tp.Equals(typeof(int)))
                {
                    query.Append(val);
                }
                else if (tp.Equals(typeof(double)))
                {
                    query.Append(val);
                }
                else
                {
                    throw new InvalidDataException("Invalid data type" + tp.GetType());
                }

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
                query = (Str(val));
            }
            else if (tp.Equals(typeof(int)))
            {
                query = (val);
            }
            else if (tp.Equals(typeof(double)))
            {
                query = (val);
            }
            else
            {
                throw new InvalidDataException("Invalid data type" + tp.GetType());
            }

            return query.ToString();
        }

        private string Str(string value)
        {
            return "'" + value + "'";
        }
    }
}
