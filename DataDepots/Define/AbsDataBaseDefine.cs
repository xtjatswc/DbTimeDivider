using DataDepots.Entity;
using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataDepots.Define
{
    public abstract class AbsDatabaseDefine<BelongToClass, DBProviderClass> : IDatabaseDefine
    {

        public Type BelongTo => typeof(BelongToClass);

        public Database Database { get; set; }

        private List<string> _isExists = new List<string>();

        public AbsDatabaseDefine()
        {
            Database = new Database
            {
                DBServer = (Depots.GetService<BelongToClass>() as AbsServerDefine).Server,
                DBProvider = Depots.GetService<DBProviderClass>() as AbsDBProvider,
                IDatabaseDefine = this
            };
            Define();
        }

        protected abstract void Define();

        public abstract void Create(string dbName);

        public void CheckExists(ExecContext context)
        {
            var dbNames = context.SqlList.Select(o => o.DatabaseName).Distinct();
            Database.DBProvider.SingleSql = new SingleSql() { DatabaseName = "master" };

            foreach (var dbName in dbNames)
            {
                if (_isExists.Contains(dbName))
                    continue;

                if (Database.DBProvider.IsDatabaseExists(dbName))
                {
                    _isExists.Add(dbName);
                    continue;
                }

                Create(dbName);
                _isExists.Add(dbName);

            }
        }
    }
}
