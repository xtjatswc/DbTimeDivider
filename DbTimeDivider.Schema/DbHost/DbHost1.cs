using DbTimeDivider.Schema;

namespace DbTimeDivider.Schema.DbHost
{
    public class DbHost1 : AbsDbHostSchema
    {
        protected override void Define()
        {
            DbHost.IP = @".";
        }
    }
}
