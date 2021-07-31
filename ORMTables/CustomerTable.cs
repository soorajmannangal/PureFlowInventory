using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class CustomerTable : ORMBase
    {
        private string name;
        public string Name { get { return name; } set { name = value; } }

        private string phone;
        public string Phone { get { return phone ?? DEFAULT_STRING; } set { phone = value; } }

        private string address;
        public string Address { get { return address ?? DEFAULT_STRING; } set { address = value; } }

        private string email;
        public string Email { get { return email ?? DEFAULT_STRING; } set { email = value; } }


        // private List<BrandGridDto> grid;
        // public List<BrandGridDto> Grid => grid ?? (grid = _dataSource.GetAllBrands(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Details), nameof(Name)));
        public List<CustomerGridDto> Grid => _dataSource.GetAllCustomers(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Phone), nameof(Address), nameof(Email), nameof(Name));

        public override eTableNames TableName => eTableNames.Customer;


        public CustomerGridDto GetCustomerByPhone(string phoneNoToMatch)
        {
           return _dataSource.GetCustomerByPhone(eGenericColumnName.ID.ToString(), nameof(Name), nameof(Phone), nameof(Address), nameof(Email), phoneNoToMatch);
        }


        public int GetIdByPhone()
        {
            return _dataSource.GetId(TableName, nameof(Phone), Phone);
        }

        public bool IsPhoneExist()
        {
            return _dataSource.GetId(TableName, nameof(Phone), Phone) != 0;
        }

        public void InsertAll()
        {
             _dataSource.Insert(TableName, nameof(Name), Name, nameof(Phone), Phone, nameof(Address), Address, nameof(Email), email);
        }
   
        //public List<ComboDto> GetBrandNames()
        //{
        //    return _dataSource.GetColumnData(TableName, nameof(Name));
        //}

        public bool IsValidForInsert()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Phone)) return false;
            return true;
        }

       
    }
}
