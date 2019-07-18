using DbTimeDivider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DBProvider
{
    public class OracleDBProvider : OracleBaseProvider
    {
        protected override string GetConnStr()
        {
            return string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={4}))(CONNECT_DATA=(SERVICE_NAME={1})));Persist Security Info=True;User ID={2};Password={3};Connect Timeout=5;",
                Database.DbHost.IP,
                Database.UID,
                CurrentQueryItem.DatabaseName,
                Database.Password,
                Database.Port
            );
        }
    }
}
