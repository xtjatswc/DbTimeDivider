using DataDepots.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepots.Entity
{
    public class ExecContext
    {

        public IDatabaseDefine IDatabaseDefine { get; set; }

        public Database Database { get; set; }

        public ITableDefine ITableDefine { get; set; }

        public Table Table { get; set; }

        public List<SingleSql> SqlList { get; set; }

        public DateTime TargetTime1 { get; set; }

        public DateTime TargetTime2 { get; set; }

    }
}
