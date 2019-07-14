using DataDepots.Core;
using DbTimeDivider.Core;
using DbTimeDivider.IFace;
using DbTimeDivider.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace DbTimeDivider.Entity
{
    public class Database
    {
        public DbHost DbHost { get; set; }

        public string Port { get; set; }

        public string ConnStr { get; set; }

        public string Name { get; set; }

        public string UID { get; set; }

        public string Password { get; set; }

        public DivisionFlag DivisionFlag { get; set; }

        public DivisionType DivisionType
        {
            get
            {
                return DivisionFlag.GetDivisionType();
            }
        }

        private Dictionary<string, Table> _tables = null;
        public Dictionary<string, Table> Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = TimeDivider.GetServices<ITableSchema>()
                        .Select(o => o.Table)
                        .Where(o => o.Database == this)
                        .ToDictionary(k => k.Name, v => v);
                }
                return _tables;
            }
        }


        public AbsDBProvider DBProvider { get; set; }

        public IDbSchema IDbSchema { get; set; }

        public string GetRealName(DateTime targetTime)
        {
            if (DivisionFlag == DivisionFlag.None)
            {
                return Name;
            }
            else
            {
                return string.Format(Name, targetTime.ToString(Enum.GetName(typeof(DivisionFlag), DivisionFlag)));
            }
        }

        public List<T> Query<T>(string sql, DateTime targetTime1)
        {
            return Query<T>(sql, targetTime1, targetTime1);
        }

        public List<T> Query<T>(string sql, DateTime targetTime1, DateTime targetTime2)
        {
            if (targetTime1 > targetTime2)
                return new List<T>();

            var context = GetDivisionContext(sql, targetTime1, targetTime2);
            return DBProvider.GetList<T>(context);

        }

        public DataTable Query(string sql, DateTime targetTime1)
        {
            return Query(sql, targetTime1, targetTime1);
        }

        public DataTable Query(string sql, DateTime targetTime1, DateTime targetTime2)
        {
            if (targetTime1 > targetTime2)
                return new DataTable();

            var context = GetDivisionContext(sql, targetTime1, targetTime2);
            return DBProvider.GetTable(context);

        }

        private DivisionContext GetDivisionContext(string sql, DateTime targetTime1, DateTime targetTime2)
        {

            DivisionContext context = new DivisionContext();
            context.IDbSchema = IDbSchema;
            context.Database = this;
            context.ITableSchemas = new List<ITableSchema>();
            context.TargetTime1 = targetTime1;
            context.TargetTime2 = targetTime2;
            context.QueryItems = new List<QueryItem>();

            //提取表名
            Regex regex = new Regex(@"『(?<tableName>.+?)』", RegexOptions.Multiline);
            var matchs = regex.Matches(sql);
           
            foreach (Match match in matchs)
            {
                string tableName = match.Groups["tableName"].Value;
                var table = Tables[tableName];

                if(!context.ITableSchemas.Contains(table.ITableSchema))
                {
                    context.ITableSchemas.Add(table.ITableSchema);
                }
            }            

            //找到粒度最小的
            var divisionType = context.ITableSchemas.Count > 0 ? context.ITableSchemas.Max(o => o.Table.DivisionType) : DivisionType;
            DateTime tempTime1 = targetTime1;
            DateTime tempTime2 = targetTime2;

            divisionType.SetTargetTime(ref tempTime1, ref tempTime2);

            while (true)
            {
                QueryItem queryItem = new QueryItem();
                queryItem.ExecSql = sql;
                queryItem.DatabaseName = GetRealName(tempTime1);

                //需要替换的表
                foreach (var tableSchema in context.ITableSchemas)
                {
                    string tableName = tableSchema.Table.GetRealName(tempTime1);
                    queryItem.TableNames.Add(tableSchema, tableName);
                    queryItem.ExecSql = queryItem.ExecSql.Replace($"『{tableSchema.Table.Name}』", tableName);
                }
                queryItem.TargetTime = tempTime1;

                context.QueryItems.Add(queryItem);

                divisionType.PlusTargetTime(ref tempTime1);

                if (tempTime1 > tempTime2)
                {
                    break;
                }
            }

            return context;
        }

    }
}
