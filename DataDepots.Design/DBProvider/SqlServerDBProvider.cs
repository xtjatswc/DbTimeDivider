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
    public class SqlServerDBProvider : AbsDBProvider
    {
        protected override string GetConnStr()
        {
            return string.Format(@"Server={0}\sqlexpress;Database={1};UID={2};Password={3};",
                Database.DBServer.IP,
                Database.Name2,
                Database.UID,
                Database.Password
            );
        }

        protected override IDbProvider GetDbProvider()
        {
            return new SqlServerProvider();
        }

        public override DataTable GetTable(string sql)
        {
            var tbl = DbContext.Sql(sql).QuerySingle<DataTable>();
            return tbl;
        }
    }
}
