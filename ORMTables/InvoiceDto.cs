using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFlow
{
    public class InvoiceDto
    {
        public int Ind { get; set; }
        public int ID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int ServiceRequestID { get; set; }    
        public int TechnicianID { get; set; }
        public string TechnicianName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }

        public DateTime NextServiceDueDate { get; set; }

        public decimal TotalAmount { get; set; }

        public String Notes { get; set; }
    }
}
