using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class InvoiceTable : ORMBase
    {
        private DateTime invoiceDate;
        public DateTime InvoiceDate { get { return invoiceDate; } set { invoiceDate = value; } }

        private int customerID;
        public int CustomerID { get { return customerID; } set { customerID = value; } }

        private int serviceRequestID;
        public int ServiceRequestID { get { return serviceRequestID; } set { serviceRequestID = value; } }

        private int serviceManID;
        public int ServiceManID { get { return serviceManID; } set { serviceManID = value; } }

        private DateTime nextServiceDueDate;
        public DateTime NextServiceDueDate { get { return nextServiceDueDate; } set { nextServiceDueDate = value; } }

        private decimal totalAmount;
        public decimal TotalAmount { get { return totalAmount; } set { totalAmount = value; } }

        private String notes;
        public String Notes { get { return notes ?? DEFAULT_STRING; } set { notes = value; } }

        public int GetLastId()
        {
            return _dataSource.GetLastId(TableName, eGenericColumnName.ID.ToString());
        }

        public ObservableCollection<InvoiceGridDto> Grid => _dataSource.GetAllInvoices(eGenericColumnName.ID.ToString(),
            nameof(CustomerID),
            nameof(InvoiceDate), 
            nameof(ServiceRequestID),
            nameof(ServiceManID), 
            nameof(NextServiceDueDate),
            nameof(TotalAmount), 
            nameof(Notes),
            nameof(InvoiceDate));


        public ObservableCollection<InvoiceGridDto> GetInvoicesForAPeriod(DateTime fromDate, DateTime toDate)
        {
            return _dataSource.GetInvoicesForAPeriod(fromDate, toDate, eGenericColumnName.ID.ToString(),
           nameof(CustomerID),
           nameof(InvoiceDate),
           nameof(ServiceRequestID),
           nameof(ServiceManID),
           nameof(NextServiceDueDate),
           nameof(TotalAmount),
           nameof(Notes),
           nameof(InvoiceDate));
        }

        public override eTableNames TableName => eTableNames.Invoice;

        public void InsertAll()
        {
            _dataSource.Insert(TableName,
                nameof(CustomerID), CustomerID,
                 nameof(InvoiceDate), InvoiceDate,
                 nameof(ServiceRequestID), ServiceRequestID,
                 nameof(ServiceManID), ServiceManID,
                 nameof(NextServiceDueDate), NextServiceDueDate,
                 nameof(Notes), Notes,
                 nameof(TotalAmount), TotalAmount);
                //nameof(Note), Note);
        }
    
    }
}
