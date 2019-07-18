using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class TablePack
    {
        public string DatabaseName { get; set; }

        public string DatabaseSuffix { get; set; }

        public string TableName { get; set; }

        public string TableSuffix { get; set; }

        public QueryItem queryItem { get; set; }
    }
}
