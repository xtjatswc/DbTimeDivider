using DataDepots;
using DataDepots.Entity;
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
        public override bool IsDatabaseExists(string dbName)
        {
            string sql = $"select count(*) from sys.databases where name = '{dbName}'";
            return DbContext.Sql(sql).QuerySingle<int>() > 0;
        }

        public override bool IsTableExists(string tableName)
        {
            string sql = $"select count(*) from sysObjects where Id=OBJECT_ID(N'{tableName}') and xtype='U'";
            return DbContext.Sql(sql).QuerySingle<int>() > 0;
        }

        protected override string GetConnStr()
        {
            return string.Format(@"Server={0};Database={1};UID={2};Password={3};",
                Database.DBServer.IP,
                SingleSql.DatabaseName,
                Database.UID,
                Database.Password
            );
        }

        protected override IDbProvider GetDbProvider()
        {
            return new SqlServerProvider();
        }


    }
}
