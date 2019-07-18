using DbTimeDivider;
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

            #region oracle
            var db = TimeDivider.GetService<OracleDB_Test>().Database;

            var tbl2 = TimeDivider.DbHosts[@"127.0.0.1"].Databases["user{0}"].Tables["ORACLE_SALE_BY_DAY_{0}"];
            string ret = tbl2.ITableSchema.GenerateEntity();



            //添加
            var parameter = new QueryPara
            {
                Sql = @"『ORACLE_SALE_BY_DAY_{0}』",
                TargetTime1 = DateTime.Now,
                UseTransaction = true,
            };
            OracleSaleByDay model3 = new OracleSaleByDay
            {
                PRODUCTID = Guid.NewGuid().ToString(),
                SYSNO = Guid.NewGuid().ToString(),
                BRANDID = Guid.NewGuid().ToString(),
                CATEGORYID = Guid.NewGuid().ToString(),
                SHOPID = Guid.NewGuid().ToString(),
                CREATEUSERID = Guid.NewGuid().ToString(),
                IMPORTGROUPID = Guid.NewGuid().ToString(),
                PRODUCTNAME = "abcd",
                CREATEDATE = DateTime.Now,
                STATISTICALDATE = DateTime.Now,
                UPDATEDATE = DateTime.Now,
                DATASOURCE = "aaa",
                OUTPRODUCTID = "ddd" + DateTime.Now.ToString()
            };
            var rows = db.Insert<OracleSaleByDay>(parameter, model3);

            //执行查询
            parameter = new QueryPara
            {
                Sql = @"select a.* from 『ORACLE_SALE_BY_DAY_{0}』 a",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                ParamSet = { { "SysNo", "99268d19-a950-4311-9aa5-017a4912d605" } },
                Parameters = { "99268d19-a950-4311-9aa5-017a4912d605" }
            };

            var tbl4 = db.Query(parameter);
            var list = db.Query<OracleSaleByDay>(parameter);

            #endregion

            #region sqlite
            db = TimeDivider.GetService<SqliteDB_Test>().Database;

            //添加
            parameter = new QueryPara
            {
                Sql = @"『SaleByDay_{0}』",
                UseTransaction = true,
            };
            SaleByDay model = new SaleByDay
            {
                ProductID = Guid.NewGuid().ToString(),
                SysNo = Guid.NewGuid().ToString(),
                BrandID = Guid.NewGuid().ToString(),
                CategoryID = Guid.NewGuid().ToString(),
                ShopID = Guid.NewGuid().ToString(),
                CreateUserID = Guid.NewGuid().ToString(),
                ImportGroupId = Guid.NewGuid().ToString(),
                ProductName = "abcd",
                CreateDate = DateTime.Now,
                StatisticalDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                DataSource = "aaa",
                OutProductID = "ddd" + DateTime.Now.ToString()
            };
            rows = db.Insert<SaleByDay>(parameter, model);

            //执行查询
            parameter = new QueryPara
            {
                Sql = @"select a.* from 『SaleByDay_{0}』 a",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                ParamSet = { { "SysNo", "9A6F3506-33FF-4436-B845-02522BE98120" } },
                Parameters = { "9A6F3506-33FF-4436-B845-02522BE98120" }
            };

            tbl4 = db.Query(parameter);
            var list2 = db.Query<SaleByDay>(parameter);

            //删除
            parameter = new QueryPara
            {
                Sql = @"delete from 『SaleByDay_{0}』 where SysNo = @0",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                Parameters = { "1be9fb12-f943-4c22-bea2-a3d51c711d02" },
                UseTransaction = true
            };
            rows = db.Execute(parameter);
            #endregion

            #region sql server
            db = TimeDivider.GetService<Lnsky_Test>().Database;

            //执行查询
            parameter = new QueryPara
            {
                Sql = @"select a.* from 『Purify_ProductSaleByDay_{0}』 a 
left join 『SaleDetail_{0}』 b on a.SysNo = b.SysNo and b.SysNo = @0 where a.SysNo = @SysNo",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                ParamSet = { { "SysNo", "9A6F3506-33FF-4436-B845-02522BE98120" } },
                Parameters = { "9A6F3506-33FF-4436-B845-02522BE98120" }
            };

            tbl4 = db.Query(parameter);
            var list3 = db.Query<Purify_ProductSaleByDay>(parameter);

            //生成实体属性
            //var tbl2 = TimeDivider.DbHosts[@".\sqlexpress"].Databases["Lnsky_Test_{0}"].Tables["Purify_ProductSaleByDay_{0}"];
            //string ret = tbl2.ITableSchema.GenerateEntity();

            //可执行添加、修改、删除
            parameter = new QueryPara
            {
                Sql = @"insert into 『SaleDetail_{0}』(SysNo) values(@0)",
                TargetTime1 = DateTime.Parse("2019-06-01"),
                TargetTime2 = DateTime.Parse("2020-05-01"),
                Parameters = { "9A6F3506-33FF-4436-B845-02522BE98120" },
                UseTransaction = true
            };
            rows = db.Execute(parameter);

            //添加
            parameter = new QueryPara
            {
                Sql = @"『Purify_ProductSaleByDay_{0}』",
                UseTransaction = true
            };
            var model2 = new Purify_ProductSaleByDay
            {
                SysNo = Guid.NewGuid(),
                ProductName = "abcd",
                CreateDate = DateTime.Now,
                StatisticalDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                DataSource = "aaa",
                OutProductID = "ddd" + DateTime.Now.ToString()
            };
            rows = db.Insert<Purify_ProductSaleByDay>(parameter, model2, x => x.ProductID);

            //修改（注：主键是必须要排除的，有唯一索引时，不要多行更新成一样的值，会报错）
            parameter = new QueryPara
            {
                Sql = @"『Purify_ProductSaleByDay_{0}』",
                UseTransaction = true
            };
            model2 = new Purify_ProductSaleByDay
            {
                SysNo = new Guid("9A6F3506-33FF-4436-B845-02522BE98120"),
                ProductName = "abcd",
                CreateDate = DateTime.Now,
                StatisticalDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                DataSource = "aaa123",
                OutProductID = "ddd" + DateTime.Now.ToString()
            };
            rows = db.Update<Purify_ProductSaleByDay>(parameter, model2, x => x.SysNo, i => i.SysNo);
            #endregion
        }


    }
}
