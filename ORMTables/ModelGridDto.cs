using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{

    public class ModelGridDto
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Details { get; set; }

        public ModelGridDto(int id, string brandName, string modelName, string details)
        {
            this.ID = id;
            this.BrandName = brandName;
            this.ModelName = modelName;
            this.Details = details;
        }
    }
}
