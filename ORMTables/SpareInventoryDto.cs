using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class SpareInventoryDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }


        public SpareInventoryDto(int id, string name, string details, int qty, DateTime lastUpdated)
        {
            this.ID = id;
            this.Name = name;
            this.Details = details;
            Quantity = qty;
            LastUpdated = lastUpdated;
        }
    }
}
