using DbTimeDivider.Core;
using DbTimeDivider.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DbHost.DbHost1_.SqliteDB_Test_
{
    public class SaleByDay : AbsTableSchema<SqliteDB_Test>
    {
        public override void Create(TablePack tablePack)
        {
            string sql = string.Format(Properties.Resources.SaleByDay, tablePack.TableName);
            Execute(sql);
        }

        protected override void Define()
        {
            Table.Name = "SaleByDay_{0}";
            Table.DivisionFlag = DivisionFlag.yyyyMM;
        }
    }
}
