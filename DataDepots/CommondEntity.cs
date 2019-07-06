using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots
{
    public class CommondEntity
    {
        public string Sql { get; set; }

        public Table TableInfo { get; set; }

        public DateTime OperateTime { get; set; }

        public IDBProvider IDBOperate2 { get; set; }
    }
}
