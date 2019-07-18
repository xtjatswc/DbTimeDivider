using DbTimeDivider.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class QueryItem
    {
        public string DatabaseName { get; set; }

        public string DatabaseSuffix { get; set; }

        public Dictionary<ITableSchema, TablePack> TableNames { get; set; } = new Dictionary<ITableSchema, TablePack>();

        public string ExecSql { get; set; }

        public DateTime TargetTime { get; set; }

    }
}
