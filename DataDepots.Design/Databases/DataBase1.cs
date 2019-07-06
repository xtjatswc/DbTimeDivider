using DataDepots;
using DataDepots.Define;

namespace DataDepotsDemo
{
    public class DataBase1 : AbsDataBaseDefine<DBServer1, SqlServerDBProvider>
    {

        protected override void Define()
        {
            Database.Name = "Lnsky_Test{0}";
            Database.UID = "sa";
            Database.Password = "sa";
            Database.DBFlag = "_yy";
        }

    }
}
