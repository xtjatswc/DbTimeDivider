using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace DbTimeDivider.IFace
{
    public abstract class SqlServerBaseProvider : AbsDBProvider
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

        protected override IDbProvider GetDbProvider()
        {
            return new SqlServerProvider();
        }
    }
}
