using DbTimeDivider.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DbHost.DbHost1_.SqliteDB_Test_
{
    public class SaleByDay : AbsTableSchema<SqliteDB_Test>
    {
        public override void Create(string tableName)
        {
            string sql = string.Format(Properties.Resources.SaleByDay, tableName);
            Execute(sql);
        }

        protected override void Define()
        {
            Table.Name = "SaleByDay_{0}";
            Table.DivisionFlag = DivisionFlag.yyyyMM;
        }
    }
}
