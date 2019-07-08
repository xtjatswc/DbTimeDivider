using DbTimeDivider;
using DbTimeDivider.Schema.DbHost.DbHost1_.Lnsky_Test_;
using System;

namespace DataDepotsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeDivider.Register("DbTimeDivider.Schema");

            //
            string sql = "select * from {0}";
            //var tbl2 = Depots.Servers["."].Databases["Lnsky_Test_{0}"].Tables["Purify_ProductSaleByDay_{0}"].Query(sql, DateTime.Now);

            var tbl = TimeDivider.GetService<Purify_ProductSaleByDay>().Table;
            //var tbl3 = tbl.Query(sql, DateTime.Now);
            var tbl4 = tbl.Query(sql, DateTime.Parse("2019-12-01"), DateTime.Parse("2020-05-01"));



        }


    }
}
