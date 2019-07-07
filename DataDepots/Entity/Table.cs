using System;
using System.Data;

namespace DataDepots
{
    public class Table
    {
        public Database Database { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public string TableFlag { get; set; }

        public DataTable Query(string sql, DateTime operateTime)
        {
            CommondEntity commond = new CommondEntity();
            commond.Sql = sql;
            commond.TableInfo = this;
            commond.OperateTime = operateTime;
            commond.IDBOperate2 = Database.DBProvider;

            DBOperate dbOpr = new DBOperate();
            var tbl = dbOpr.GetTable(commond);
            return tbl;
        }
    }
}
