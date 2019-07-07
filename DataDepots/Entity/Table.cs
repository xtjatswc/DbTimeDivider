using System;
using System.Data;

namespace DataDepots
{
    public class Table
    {
        public Database Database { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public string TableFlag { get; set; }

        public DataTable Query(string sql, DateTime time)
        {
            Database.Name2 = string.Format(Database.Name, time.ToString(Database.DBFlag));
            Name2 = string.Format(Name, time.ToString(TableFlag));
            sql = string.Format(sql, this.Name2);

            return Database.DBProvider.GetTable(this, sql);
        }
    }
}
