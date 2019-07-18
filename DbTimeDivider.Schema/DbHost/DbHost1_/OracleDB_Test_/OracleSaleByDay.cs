using DbTimeDivider.Core;
using DbTimeDivider.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DbHost.DbHost1_.OracleDB_Test_
{
    public class OracleSaleByDay : AbsTableSchema<OracleDB_Test>
    {
        public override void Create(TablePack tablePack)
        {
            string sql = string.Format(Properties.Resources.Oracle_SaleByDay, tablePack.TableName, tablePack.DatabaseSuffix);

            SplitExecute(sql, ";\r\n");

        }

        protected override void Define()
        {
            //oracle表名要求大写
            Table.Name = "ORACLE_SALE_BY_DAY_{0}";
            Table.DivisionFlag = DivisionFlag.yyMM;
        }
    }
}
