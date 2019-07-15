using DbTimeDivider.IFace;

namespace DbTimeDivider.Schema.DBProvider
{
    public class SqlServerDBProvider : SqlServerBaseProvider
    {
        protected override string GetConnStr()
        {
            return string.Format(@"Server={0};Database={1};UID={2};Password={3};",
                Database.DbHost.IP,
                CurrentQueryItem.DatabaseName,
                Database.UID,
                Database.Password
            );
        }
    }
}
