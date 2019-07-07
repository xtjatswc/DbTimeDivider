using DataDepots;
using DataDepots.Util;
using System;

namespace DataDepotsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Depots.Register("DataDepots.Design");

            //
            string sql = "select * from {0}";
            var tbl2 = Depots.Servers["."].Databases["Lnsky_Test{0}"].Tables["Purify_ProductSaleByDay{0}"].Query(sql, DateTime.Now);

            var tbl = Depots.iContainer.GetService<Purify_ProductSaleByDay>().Table;
            var tbl3 = tbl.Query(sql, DateTime.Now);
        }


    }
}
