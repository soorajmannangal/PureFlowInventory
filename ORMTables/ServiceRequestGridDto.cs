using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFlow
{
    public class ServiceRequestGridDto
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int BrandID { get; set; }
        public int ModelID { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public bool IsUnderWarranty { get; set; }
        public DateTime DateOfEntry { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResolvedDate { get; set; }

    }
}
