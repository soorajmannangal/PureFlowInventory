using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFlow
{
    public class InvoiceItemsDto
    {
        public int Ind { get; set; }
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int InventoryID { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public int WorkTypeID { get; set; }
        public string ServiceType { get; set; }
        public decimal Amount { get; set; }

    }
}
