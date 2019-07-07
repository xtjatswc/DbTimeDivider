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
            //var tbl2 = Depots.Servers["."].Databases["Lnsky_Test_{0}"].Tables["Purify_ProductSaleByDay_{0}"].Query(sql, DateTime.Now);

            var tbl = Depots.GetService<Purify_ProductSaleByDay>().Table;
            //var tbl3 = tbl.Query(sql, DateTime.Now);
            var tbl4 = tbl.Query(sql, DateTime.Now.AddMonths(-2), DateTime.Now.AddHours(-1));



        }


    }
}
