using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class BrandListView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public BrandListView(int id, string name, string details)
        {
            this.ID = id;
            this.Name = name;
            this.Details = details;
        }
    }
}
