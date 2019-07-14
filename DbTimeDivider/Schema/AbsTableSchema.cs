using DbTimeDivider.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DbTimeDivider.Schema
{
    public abstract class AbsTableSchema<BelongToClass> : ITableSchema
    {
        public Table Table { get; set; }
        private List<string> _isExists = new List<string>();

        public AbsTableSchema()
        {
            Table = new Table
            {
                Database = (TimeDivider.GetService<BelongToClass>() as IDbSchema).Database,
                ITableSchema = this
            };
            Define();
        }

        protected abstract void Define();

        public abstract void Create(string tableName);

        public void CheckExists(DivisionContext context)
        {
            foreach (var queryItem in context.QueryItems)
            {
                var tableName = queryItem.TableNames[this];
                string existsKey = $"{queryItem.DatabaseName}=>{tableName}";

                if (_isExists.Contains(existsKey))
                    continue;

                Table.Database.DBProvider.CurrentQueryItem = queryItem;
                if (Table.Database.DBProvider.IsTableExists(tableName))
                {
                    _isExists.Add(existsKey);
                    continue;
                }

                Create(tableName);
                _isExists.Add(existsKey);
            }

        }

        public string GenerateEntity()
        {
            QueryPara queryPara = new QueryPara
            {
                Sql = $"select * from 『{Table.Name}』 where 1=2"
            };
            DataTable tbl = Table.Database.Query(queryPara);
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn column in tbl.Columns)
            {
                sb.AppendLine();
                sb.AppendLine("public " + column.DataType.Name + " " + column.ColumnName + "{ get; set; }");
            }
            return sb.ToString();

        }
    }
}
