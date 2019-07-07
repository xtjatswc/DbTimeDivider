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

        public DataTable Query(string sql, DateTime operateTime)
        {
            DBRoute dBRoute = new DBRoute();

            dBRoute.GetTableInfo(this, operateTime);

            sql = string.Format(sql, this.Name2);

            return Database.DBProvider.GetTable(this, sql);
        }
    }
}
