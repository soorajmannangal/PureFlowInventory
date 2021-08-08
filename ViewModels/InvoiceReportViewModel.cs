using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace PureFlow
{
    public class InvoiceReportViewModel : ViewModelBase
    {

        private readonly InvoiceTable invoiceTable;
         public InvoiceReportViewModel(ICommand enableMainWindowCommand) : base(enableMainWindowCommand)
        {
            invoiceTable = new InvoiceTable();
            Init();
        }

        private void Init()
        {
            Grid = new ObservableCollection<InvoiceDto>();
            FromDate = DateTime.Now;
            ToDate = DateTime.Now.AddDays(1);
          

        }

        private void LoadServiceDueGrid()
        {
            if (Grid == null) return;

            Grid.Clear();
            decimal total = 0;
            foreach (var item in invoiceTable.GetInvoiceForAPeriod(FromDate, ToDate))
            {
                total += item.TotalAmount;
                Grid.Add(item);
            }
            TotalAmount = total;
        }


        private ObservableCollection<InvoiceDto> invoicereport;
        public ObservableCollection<InvoiceDto> Grid
        {
            get => invoicereport;
            set
            {
                invoicereport = value; 
                OnPropertyChanged(nameof(Grid));
            }
        }

        private DateTime fromDate;
        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; LoadServiceDueGrid(); OnPropertyChanged(nameof(FromDate)); }
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; LoadServiceDueGrid(); OnPropertyChanged(nameof(ToDate)); }
        }

        private decimal totalAmount;
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
        }


        public override void SetDefaults()
        {
            throw new NotImplementedException();
        }
       
    }
}
