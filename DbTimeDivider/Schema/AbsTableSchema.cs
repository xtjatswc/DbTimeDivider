using DbTimeDivider.Entity;
using System.Collections.Generic;

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
    }
}
