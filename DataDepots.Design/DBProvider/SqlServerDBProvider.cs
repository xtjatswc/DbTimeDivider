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

        public DataTable GetTable(CommondEntity commond, string sql)
        {
            IDbContext dbContext = null;
            if (dbCache.ContainsKey(commond.TableInfo.Database))
            {
                dbContext = dbCache[commond.TableInfo.Database];
            }
            else
            {
                string connStr = string.Format(@"Server={0}\sqlexpress;Database={1};UID={2};Password={3};",
                    commond.TableInfo.Database.DBServer.IP,
                    commond.TableInfo.Database.Name2,
                    commond.TableInfo.Database.UID,
                    commond.TableInfo.Database.Password
                );

                dbContext = new DbContext().ConnectionString(connStr, new SqlServerProvider());
                dbCache.Add(commond.TableInfo.Database, dbContext);
            }

            var tbl = dbContext.Sql(sql).QuerySingle<DataTable>();
            return tbl;
        }
    }
}
