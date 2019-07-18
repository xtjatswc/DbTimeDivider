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

        public abstract void Create(TablePack tablePack);

        public void CheckExists(DivisionContext context)
        {
            foreach (var dbQueryItem in context.QueryItems)
            {
                foreach (var queryItem in dbQueryItem.Value)
                {
                    var tablePack = queryItem.TableNames[this];
                    string existsKey = $"{queryItem.DatabaseName}=>{tablePack.TableName}";

                    if (_isExists.Contains(existsKey))
                        continue;

                    Table.Database.DBProvider.CurrentQueryItem = queryItem;
                    if (Table.Database.DBProvider.IsTableExists(tablePack.TableName))
                    {
                        _isExists.Add(existsKey);
                        continue;
                    }

                    Create(tablePack);
                    _isExists.Add(existsKey);
                }
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

        public void Execute(string sql)
        {
            Table.Database.DBProvider.DbContext.Sql(sql).Execute();
        }

        public void SplitExecute(string sql, string separator, bool useTransaction = true)
        {
            string[] arr = sql.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            using (var context = Table.Database.DBProvider.DbContext.UseTransaction(useTransaction))
            {
                foreach (var item in arr)
                {
                    context.Sql(item).Execute();
                }
                context.Commit();
            }

        }

    }
}
