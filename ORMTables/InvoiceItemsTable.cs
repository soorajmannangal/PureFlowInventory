using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class InvoiceItemsTable : ORMBase
    {
        private int invoiceID;
        public int InvoiceID { get { return invoiceID; } set { invoiceID = value; } }

        private int spareInventoryID;
        public int SpareInventoryID { get { return spareInventoryID; } set { spareInventoryID = value; } }

        private int qty;
        public int Qty { get { return qty; } set { qty = value; } }

        private int workTypeID;
        public int WorkTypeID { get { return workTypeID; } set { workTypeID = value; } }

        private decimal amount;
        public decimal Amount { get { return amount; } set { amount = value; } }

        public List<InvoiceItemsGridDto> GetInvoiceItems(int reqInvoiceID)
        {
            return _dataSource.GetInvoiceItems(eGenericColumnName.ID.ToString(),
             nameof(InvoiceID),
             nameof(SpareInventoryID),
             nameof(Qty),
             nameof(WorkTypeID),
             nameof(Amount),
             reqInvoiceID);
        }

        public override eTableNames TableName => eTableNames.InvoiceItems;

        public void InsertAll()
        {
            _dataSource.Insert(TableName,
                nameof(InvoiceID), InvoiceID,
                nameof(SpareInventoryID), SpareInventoryID,
                nameof(Qty), Qty,
                nameof(WorkTypeID), WorkTypeID,
                nameof(Amount), Amount);
        }
    
    }
}
