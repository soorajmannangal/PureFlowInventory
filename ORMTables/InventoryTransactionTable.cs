using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class InventoryTransactionTable : ORMBase
    {
        public override eTableNames TableName => eTableNames.InventoryTransaction;
    
        private int spareInventoryID;
        public int SpareInventoryID  { get => spareInventoryID; set => spareInventoryID = value; }

        private int qty;
        public int Qty { get => qty; set => qty = value; }

        private int userID;
        public int UserID { get => userID;  set => userID = value; }

        private DateTime transactionDate;
        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }

        public List<InventoryTransactionDto> Grid => _dataSource.GetInventoryTransactionData(
            eGenericColumnName.ID.ToString(), nameof(SpareInventoryID), nameof(Qty), nameof(UserID), nameof(TransactionDate), nameof(transactionDate));

        public InventoryTransactionTable()
        {

        }

        public InventoryTransactionTable(int spareInvId, int qty, int userId)
        {
            transactionDate = DateTime.Now;
            this.spareInventoryID = spareInvId;
            this.qty = qty;
            this.userID = userId;
        }

        public void InsertAll()
        {
            _dataSource.Insert(TableName, nameof(SpareInventoryID), SpareInventoryID, 
                nameof(Qty), Qty, nameof(UserID), UserID, nameof(TransactionDate), transactionDate);
        }
    }
}
