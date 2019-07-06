using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataDepots
{
    public class DBOperate : IDBOperate
    {
        public DataTable GetTable(CommondEntity commond)
        {
            DBRoute dBRoute = new DBRoute();

            commond.TableInfo = dBRoute.GetTableInfo(commond.TableInfo, commond.OperateTime);

            commond.Sql = string.Format(commond.Sql, commond.TableInfo.TableName2);

            return commond.IDBOperate2.GetTable(commond, commond.Sql);
        }
    }
}
