using DbTimeDivider;
using DbTimeDivider.Entity;
using DbTimeDivider.Schema.DbHost.DbHost1_;
using DbTimeDivider.Schema.DbHost.DbHost1_.Lnsky_Test_;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataDepotsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeDivider.Register("DbTimeDivider.Schema");
            var db = TimeDivider.GetService<Lnsky_Test>().Database;

            //执行查询
            QueryPara parameter = new QueryPara
            {
                Sql = @"select a.* from 『Purify_ProductSaleByDay_{0}』 a 
left join 『SaleDetail_{0}』 b on a.SysNo = b.SysNo and b.SysNo = @0 where a.SysNo = @SysNo",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                ParamSet = { { "SysNo", "9A6F3506-33FF-4436-B845-02522BE98120" } },
                Parameters = { "9A6F3506-33FF-4436-B845-02522BE98120" }
            };

            var tbl4 = db.Query(parameter);
            var list = db.Query<Purify_ProductSaleByDay>(parameter);

            //生成实体属性
            //var tbl2 = TimeDivider.DbHosts[@".\sqlexpress"].Databases["Lnsky_Test_{0}"].Tables["Purify_ProductSaleByDay_{0}"];
            //string ret = tbl2.ITableSchema.GenerateEntity();

            //执行添加操作
            parameter = new QueryPara
            {
                Sql = @"insert into 『SaleDetail_{0}』(SysNo) values(@0)",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                Parameters = { "9A6F3506-33FF-4436-B845-02522BE98120" },
                UseTransaction = true
            };
            var rows = db.Execute(parameter);
        }


    }
}
