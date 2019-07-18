using DbTimeDivider.Core;
using DbTimeDivider.Schema.DBProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DbHost.DbHost1_
{
    public class SqliteDB_Test : AbsDbSchema<DbHost1, SqliteDBProvider>
    {
        public override void Create(string dbName)
        {
            //throw new NotImplementedException();
        }

        protected override void Define()
        {
            Database.Name = AppDomain.CurrentDomain.BaseDirectory + @"db\SqliteDB_Test_{0}.db";
            Database.DivisionFlag = DivisionFlag.yy;
        }
    }
}
