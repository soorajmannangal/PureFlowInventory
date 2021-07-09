using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class JoinTable : ORMBase
    {
        public override eTableNames TableName => throw new NotImplementedException();

        public List<ModelDto> GetModelBrandData()
        {
            return new List<ModelDto>();
        }
    }
}
