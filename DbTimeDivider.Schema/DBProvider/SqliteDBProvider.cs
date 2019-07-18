using DbTimeDivider.IFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DBProvider
{
    public class SqliteDBProvider : SqliteBaseProvider
    {
        protected override string GetConnStr()
        {
            return string.Format("Data Source={0};Version=3;",
                CurrentQueryItem.DatabaseName
            );
        }
    }
}
