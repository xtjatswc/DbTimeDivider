using DbTimeDivider;
using DbTimeDivider.Schema.DbHost.DbHost1_;
using System;

namespace DataDepotsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeDivider.Register("DbTimeDivider.Schema");

            //
            string sql = @"select * from 『Purify_ProductSaleByDay_{0}』 a 
left join 『SaleDetail_{0}』 b on a.SysNo = b.SysNo";
            //var tbl2 = Depots.Servers["."].Databases["Lnsky_Test_{0}"].Tables["Purify_ProductSaleByDay_{0}"].Query(sql, DateTime.Now);

            var db = TimeDivider.GetService<Lnsky_Test>().Database;
            //var tbl3 = tbl.Query(sql, DateTime.Now);
            var tbl4 = db.Query(sql, DateTime.Parse("2019-06-01"), DateTime.Parse("2020-05-01"));



        }


    }
}
