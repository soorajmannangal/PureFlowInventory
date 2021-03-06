using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class InventoryTransactionDto
    {
        public int Ind { get; set; }
        public int ID { get; set; }
        public int SpareInventoryID { get; set; }
        public string SpareName { get; set; }
        public int Qty { get; set; }
        public int UserID { get; set; }   
        public string UserName { get; set; }   
        public DateTime TransactionDate { get; set; }    
    }
}
