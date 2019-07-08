using DataDepots.Define;
using DataDepots.Entity;
using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots.IFace
{
    public abstract class AbsTableDefine<BelongToClass> : ITableDefine
    {
        public Table Table { get; set; }
        private List<string> _isExists = new List<string>();

        public AbsTableDefine()
        {
            Table = new Table
            {
                Database = (Depots.GetService<BelongToClass>() as IDatabaseDefine).Database,
                ITableDefine = this
            };
            Define();
        }

        protected abstract void Define();

        public abstract void Create(string tableName);

        public void CheckExists(ExecContext context)
        {
            foreach (var singleSql in context.SqlList)
            {
                string existsKey = $"{singleSql.DatabaseName}=>{singleSql.TableName}";

                if (_isExists.Contains(existsKey))
                    continue;

                Table.Database.DBProvider.SingleSql = singleSql;
                if (Table.Database.DBProvider.IsTableExists(singleSql.TableName))
                {
                    _isExists.Add(existsKey);
                    continue;
                }

                Create(singleSql.TableName);
                _isExists.Add(existsKey);
            }

        }
    }
}
