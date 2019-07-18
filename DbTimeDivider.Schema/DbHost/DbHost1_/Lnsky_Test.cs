using DbTimeDivider.Core;
using DbTimeDivider.Entity;
using DbTimeDivider.Schema.DBProvider;

namespace DbTimeDivider.Schema.DbHost.DbHost1_
{
    public class Lnsky_Test : AbsDbSchema<DbHost1, SqlServerDBProvider>
    {

        public override void Create(QueryItem queryItem)
        {
            string sql = $"Create database {queryItem.DatabaseName}";
            Execute(sql);
        }

        protected override void Define()
        {
            Database.Name = "Lnsky_Test_{0}";
            Database.UID = "sa";
            Database.Password = "sa";
            Database.DivisionFlag = DivisionFlag.yy;
        }

        protected override QueryItem GetDefaultQueryItem()
        {
            return new QueryItem { DatabaseName = "master" };
        }
    }
}
