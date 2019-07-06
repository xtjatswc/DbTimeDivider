using DataDepots.Define;
using DataDepots.Util;
using System.Collections.Generic;

namespace DataDepots
{
    public class Database
    {
        public DBServer DBServer { get; set; }

        public string Port { get; set; }

        public string ConnStr { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public string UID { get; set; }

        public string Password { get; set; }

        public string DBFlag { get; set; }

        public Dictionary<string, Table> Table = new Dictionary<string, Table>();

        public IDBProvider DBProvider { get; set; }

        internal Database AddTable()
        {

            var tbls = Depots.iContainer.GetServices<ITableDefine>();
            foreach (var tbl in tbls)
            {
                var table = tbl.Table;
                this.Table.Add(table.TableName, table);

            }

            return this;
        }
    }
}
