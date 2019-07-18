using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace DbTimeDivider.Provider
{
    public abstract class OracleBaseProvider : AbsDBProvider
    {
        public override bool IsDatabaseExists(string dbName)
        {
            string sql = $"SELECT count(*) FROM ALL_USERS WHERE USERNAME = upper('{dbName}')";
            return DbContext.Sql(sql).QuerySingle<int>() > 0;
        }

        public override bool IsTableExists(string tableName)
        {
            string sql = $"select count(*) from user_tables where table_name ='{tableName}'";
            return DbContext.Sql(sql).QuerySingle<int>() > 0;
        }

        protected override IDbProvider GetDbProvider()
        {
            return new OracleProvider();
        }
    }
}
