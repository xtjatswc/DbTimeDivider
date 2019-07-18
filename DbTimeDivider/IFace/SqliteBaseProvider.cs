using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentData;

namespace DbTimeDivider.IFace
{
    public abstract class SqliteBaseProvider : AbsDBProvider
    {
        public override bool IsDatabaseExists(string dbName)
        {
            return File.Exists(dbName);
        }

        public override bool IsTableExists(string tableName)
        {
            string sql = $"SELECT COUNT(*) as CNT FROM sqlite_master WHERE type = 'table' AND name = '{tableName}'";
            return DbContext.Sql(sql).QuerySingle<int>() > 0;
        }

        protected override IDbProvider GetDbProvider()
        {
            return new SqliteProvider();
        }
    }
}
