using DataDepots.Entity;
using System;
using System.Data;

namespace DataDepots
{
    public class Table
    {
        public Database Database { get; set; }

        public string Name { get; set; }

        public string TableFlag { get; set; }

        public DataTable Query(string sql, DateTime time)
        {
            ExecContext context = new ExecContext();
            context.Table = this;
            context.DatabaseName = string.Format(Database.Name, time.ToString(Database.DBFlag));
            context.TableName = string.Format(Name, time.ToString(TableFlag));
            context.ExecSql = string.Format(sql, context.TableName);

            return Database.DBProvider.GetTable(context);
        }
    }
}
