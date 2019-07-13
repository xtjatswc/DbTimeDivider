using DbTimeDivider.Schema;
using System;
using System.Collections.Generic;

namespace DbTimeDivider.Entity
{
    public class DivisionContext
    {

        public IDbSchema IDbSchema { get; set; }

        public Database Database { get; set; }

        public List<ITableSchema> ITableSchemas { get; set; }

        public List<QueryItem> QueryItems { get; set; }

        public DateTime TargetTime1 { get; set; }

        public DateTime TargetTime2 { get; set; }

    }
}
