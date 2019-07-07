using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepots.Entity
{
    public class ExecContext
    {
        public string DatabaseName { get; set; }

        public string TableName { get; set; }

        public Table Table { get; set; }

        public string ExecSql { get; set; }

    }
}
