using DbTimeDivider.Core;
using DbTimeDivider.IFace;
using DbTimeDivider.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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

        public List<T> Query<T>(QueryPara parameter)
        {
            if (parameter.TargetTime1 > parameter.TargetTime2)
                return new List<T>();

            var context = GetDivisionContext(parameter);
            return DBProvider.GetList<T>(context);

        }

        public DataTable Query(QueryPara parameter)
        {
            if (parameter.TargetTime1 > parameter.TargetTime2)
                return new DataTable();

            var context = GetDivisionContext(parameter);
            return DBProvider.GetTable(context);

        }

        public int Execute(QueryPara parameter)
        {
            var context = GetDivisionContext(parameter);
            return DBProvider.Execute(context);
        }

        public int Insert<T>(QueryPara parameter, T t, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var context = GetDivisionContext(parameter);
            return DBProvider.Insert(context, t, ignoreProperties);
        }

        public int Update<T>(QueryPara parameter, T t, Expression<Func<T, object>> whereProperties, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var context = GetDivisionContext(parameter);
            return DBProvider.Update(context, t, whereProperties, ignoreProperties);
        }


        private DivisionContext GetDivisionContext(QueryPara parameter)
        {

            DivisionContext context = new DivisionContext();
            context.IDbSchema = IDbSchema;
            context.Database = this;
            context.ITableSchemas = new List<ITableSchema>();
            context.QueryPara = parameter;

            //提取表名
            Regex regex = new Regex(@"『(?<tableName>.+?)』", RegexOptions.Multiline);
            var matchs = regex.Matches(parameter.Sql);

            foreach (Match match in matchs)
            {
                string tableName = match.Groups["tableName"].Value;
                var table = Tables[tableName];

                if (!context.ITableSchemas.Contains(table.ITableSchema))
                {
                    context.ITableSchemas.Add(table.ITableSchema);
                }
            }

            //找到粒度最小的
            var divisionType = context.ITableSchemas.Count > 0 ? context.ITableSchemas.Max(o => o.Table.DivisionType) : DivisionType;
            DateTime tempTime1 = parameter.TargetTime1;
            DateTime tempTime2 = parameter.TargetTime2;

            divisionType.SetTargetTime(ref tempTime1, ref tempTime2);

            var tempQueryItems = new List<QueryItem>();
            while (true)
            {
                QueryItem queryItem = new QueryItem();
                queryItem.ExecSql = parameter.Sql;
                queryItem.DatabaseName = GetRealName(tempTime1);

                //需要替换的表
                foreach (var tableSchema in context.ITableSchemas)
                {
                    string tableName = tableSchema.Table.GetRealName(tempTime1);
                    queryItem.TableNames.Add(tableSchema, tableName);
                    queryItem.ExecSql = queryItem.ExecSql.Replace($"『{tableSchema.Table.Name}』", $" {tableName} ");
                }
                queryItem.TargetTime = tempTime1;

                tempQueryItems.Add(queryItem);

                divisionType.PlusTargetTime(ref tempTime1);

                if (tempTime1 > tempTime2)
                {
                    break;
                }
            }

            context.QueryItems = tempQueryItems.GroupBy(o => o.DatabaseName).ToDictionary(g => g.Key, g => g.ToList());

            return context;
        }

    }
}
