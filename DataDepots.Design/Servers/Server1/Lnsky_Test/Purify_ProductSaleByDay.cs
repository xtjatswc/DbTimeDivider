using DataDepots;
using DataDepots.Core;
using DataDepots.IFace;

namespace DataDepotsDemo
{
    public class Purify_ProductSaleByDay : AbsTableDefine<Lnsky_Test>
    {
        protected override void Define()
        {
            Table.Name = "Purify_ProductSaleByDay_{0}";
            Table.DepotsFlag = DepotsFlag.MM;
        }
    }
}
