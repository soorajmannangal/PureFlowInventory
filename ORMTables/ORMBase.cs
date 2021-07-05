using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public abstract class ORMBase : IORMTable
    {
        protected readonly IDataSource _dataSource;

        protected const string DEFAULT_STRING = "NA";
        protected const int DEFAULT_INT = -1;


        public ORMBase()
        {
            _dataSource = DBHelper.GetInstance().DataSource;
        }
        public abstract eTableNames TableName { get; }

       
    }
}
