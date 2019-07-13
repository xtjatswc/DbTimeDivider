using DataDepots.Core;
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

        public IEnumerable<DataRow> Query(string sql, DateTime targetTime1)
        {
            return Query(sql, targetTime1, targetTime1);
        }

        public IEnumerable<DataRow> Query(string sql, DateTime targetTime1, DateTime targetTime2)
        {
            if (targetTime1 > targetTime2)
                throw new Exception("参数：targetTime1必须小于或等于targetTime2");

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

            if (matchs.Count == 0)
                throw new Exception("从sql中未匹配到任何表");
           
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
            var divisionType = context.ITableSchemas.Max(o => o.Table.DivisionType);
            DateTime tempTime1 = targetTime1;
            DateTime tempTime2 = targetTime2;

            switch (divisionType)
            {
                case DivisionType.Year:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-01-01"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-01-01")).AddYears(1).AddSeconds(-1);
                    break;
                case DivisionType.Month:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-01"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-01")).AddMonths(1).AddSeconds(-1);
                    break;
                case DivisionType.Day:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd")).AddDays(1).AddSeconds(-1);
                    break;
                case DivisionType.Hour:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd HH:00:00"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd HH:00:00")).AddHours(1).AddSeconds(-1);
                    break;
                default:
                    break;
            }

            while (true)
            {
                QueryItem queryItem = new QueryItem();
                queryItem.ExecSql = sql;
                queryItem.DatabaseName = string.Format(Name, tempTime1.ToString(Enum.GetName(typeof(DivisionFlag), DivisionFlag)));

                //需要替换的表
                var tableSchemas = context.ITableSchemas.Where(o => o.Table.DivisionType == divisionType);
                foreach (var tableSchema in tableSchemas)
                {
                    string tableName = string.Format(tableSchema.Table.Name, tempTime1.ToString(Enum.GetName(typeof(DivisionFlag), tableSchema.Table.DivisionFlag)));
                    queryItem.TableNames.Add(tableSchema, tableName);
                    queryItem.ExecSql = queryItem.ExecSql.Replace($"『{tableSchema.Table.Name}』", tableName);
                }
                queryItem.TargetTime = tempTime1;

                context.QueryItems.Add(queryItem);

                switch (divisionType)
                {
                    case DivisionType.Year:
                        tempTime1 = tempTime1.AddYears(1);
                        break;
                    case DivisionType.Month:
                        tempTime1 = tempTime1.AddMonths(1);
                        break;
                    case DivisionType.Day:
                        tempTime1 = tempTime1.AddDays(1);
                        break;
                    case DivisionType.Hour:
                        tempTime1 = tempTime1.AddHours(1);
                        break;
                    default:
                        break;
                }

                if (tempTime1 > tempTime2)
                {
                    break;
                }
            }

            return DBProvider.GetTable(context);
        }

    }
}
