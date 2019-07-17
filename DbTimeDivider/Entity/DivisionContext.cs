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

        public Dictionary<string, List<QueryItem>> QueryItems { get; set; }

        public QueryPara QueryPara { get; set; }

    }
}
