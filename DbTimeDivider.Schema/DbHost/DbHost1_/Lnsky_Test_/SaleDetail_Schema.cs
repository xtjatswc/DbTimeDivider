using DataDepots.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DbHost.DbHost1_.Lnsky_Test_
{
    public class SaleDetail_Schema : AbsTableSchema<Lnsky_Test>
    {

        protected override void Define()
        {
            Table.Name = "SaleDetail_{0}";
            Table.DivisionFlag = DivisionFlag.MM;
        }

        public override void Create(string tableName)
        {
            string sql = @"CREATE TABLE [dbo].[{0}] (
  [ID] int  IDENTITY(1,1) NOT NULL,
  [SysNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)";
            sql = string.Format(sql, tableName);
            try
            {
                Table.Database.DBProvider.DbContext.Sql(sql).Execute();
            }
            catch
            {
            }
        }

    }
}
