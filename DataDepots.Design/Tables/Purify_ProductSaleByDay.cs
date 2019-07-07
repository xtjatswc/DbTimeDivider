using DataDepots;
using DataDepots.IFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepotsDemo
{
    public class Purify_ProductSaleByDay : AbsTableDefine<DataBase1>
    {
        protected override void Define()
        {
            Table.Name = "Purify_ProductSaleByDay{0}";
            Table.TableFlag = "_MM";
        }
    }
}
