using DataDepots;
using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepotsDemo
{
    public class SqlServerDBProvider : IDBProvider
    {
        Dictionary<Database, IDbContext> dbCache = new Dictionary<Database, IDbContext>();

        public DataTable GetTable(Table table, string sql)
        {
            IDbContext dbContext = null;
            if (dbCache.ContainsKey(table.Database))
            {
                dbContext = dbCache[table.Database];
            }
            else
            {
                string connStr = string.Format(@"Server={0}\sqlexpress;Database={1};UID={2};Password={3};",
                    table.Database.DBServer.IP,
                    table.Database.Name2,
                    table.Database.UID,
                    table.Database.Password
                );

                dbContext = new DbContext().ConnectionString(connStr, new SqlServerProvider());
                dbCache.Add(table.Database, dbContext);
            }

            var tbl = dbContext.Sql(sql).QuerySingle<DataTable>();
            return tbl;
        }
    }
}
