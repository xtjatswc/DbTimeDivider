using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots
{
    public class DBRoute
    {
        public Table GetTableInfo(Table tableInfo, DateTime time)
        {
            var tb = tableInfo;
            tb.Database.Name2 = string.Format(tb.Database.Name, time.ToString(tableInfo.Database.DBFlag));
            tb.Name2 = string.Format(tb.Name, time.ToString(tableInfo.TableFlag));

            return tb;
        }
    }
}
