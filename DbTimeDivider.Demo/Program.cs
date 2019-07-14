﻿using DbTimeDivider;
using DbTimeDivider.Entity;
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
            string sql = @"select a.* from 『Purify_ProductSaleByDay_{0}』 a 
left join 『SaleDetail_{0}』 b on a.SysNo = b.SysNo where a.SysNo = @0";

            var db = TimeDivider.GetService<Lnsky_Test>().Database;
            var tbl4 = db.Query(sql, DateTime.Parse("2019-06-01"), DateTime.Parse("2020-05-01"), "9A6F3506-33FF-4436-B845-02522BE98120");
            var list = db.Query<Purify_ProductSaleByDay>(sql, DateTime.Parse("2019-06-01"), DateTime.Parse("2020-05-01"), "9A6F3506-33FF-4436-B845-02522BE98120");

            //var tbl2 = TimeDivider.DbHosts[@".\sqlexpress"].Databases["Lnsky_Test_{0}"].Tables["Purify_ProductSaleByDay_{0}"];
            //string ret = tbl2.ITableSchema.GenerateEntity(DateTime.Now);

        }


    }
}
