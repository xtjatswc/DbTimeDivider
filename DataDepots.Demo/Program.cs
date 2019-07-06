using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using DataDepots;
using DataDepots.Util;
using FluentData;

namespace DataDepotsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Depots.Register("DataDepots.Design");

            //
            string sql = "select * from {0}";
            //var tbl2 = Depots.DBServer["."].DataBase["Lnsky_Test"].Table["Purify_ProductSaleByDay"].Query(sql, DateTime.Now);
            var tbl = Depots.iContainer.GetService<Purify_ProductSaleByDay>().Table;
            var tbl2 = tbl.Query(sql, DateTime.Now);
        }


    }
}
