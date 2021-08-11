using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class UserTable : ORMBase
    {      
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string phone;
        public string Phone { get { return phone; } set { phone = value; } }

        private string password;
        public string Password { get { return password ?? DEFAULT_STRING; } set { password = value; } }

        private bool isActive;
        public bool IsActive { get => isActive; set { isActive = value; } }
        private bool isAdmin;
        public bool IsAdmin { get => isAdmin; set { isAdmin = value; } }

        public UserTable()
        {
            isActive = true;
        }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(Name), Name, nameof(Phone), Phone, nameof(Password), Password, nameof(IsActive), IsActive, nameof(IsAdmin), IsAdmin);
        }

        public List<ComboDto> GetItemNames()
        {
            return _dataSource.GetColumnData(TableName, nameof(Name));
        }

        public bool CheckIsAdmin(int userId)
        {
            return _dataSource.GetBoolColWithID(TableName, userId, eUserTable.IsAdmin.ToString());
        }

        public int CheckPassword(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) return -1;          
            username = username.ToUpper();
            int id = _dataSource.GetId(TableName, nameof(Name), username);
            if (id == 0) return -1;
            var res = _dataSource.GetColumnDataById(TableName, nameof(Password), id);

            if (res == null || res.Count == 0) return -1;

            if (!res[0].Field.Equals(password.Trim())) return -1;
           
            return id;
        }

        public bool IsValidForInsert()
        {
            if (String.IsNullOrEmpty(Name)) return false;
            return true;
        }

        public bool IsItemNameExist()
        {
            return _dataSource.GetId(TableName, nameof(Name), Name) != 0;
        }

        public override eTableNames TableName => eTableNames.UserTable;
    }
}
