using DbTimeDivider.Entity;
using DbTimeDivider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbTimeDivider.Schema
{
    public abstract class AbsDbSchema<BelongToClass, DBProviderClass> : IDbSchema
    {

        public Type BelongTo => typeof(BelongToClass);

        public Database Database { get; set; }

        private QueryItem _defaultQueryItem;
        public QueryItem DefaultQueryItem
        {
            get
            {
                if (_defaultQueryItem == null)
                {
                    _defaultQueryItem = GetDefaultQueryItem();
                }

                return _defaultQueryItem;
            }
        }

        protected abstract QueryItem GetDefaultQueryItem();

        private List<string> _isExists = new List<string>();

        public AbsDbSchema()
        {
            Database = new Database
            {
                DbHost = (TimeDivider.GetService<BelongToClass>() as AbsDbHostSchema).DbHost,
                DBProvider = TimeDivider.GetService<DBProviderClass>() as AbsDBProvider,
                IDbSchema = this
            };
            Define();
        }

        protected abstract void Define();

        public abstract void Create(QueryItem queryItem);

        public void CheckExists(DivisionContext context)
        {
            var dbNames = context.QueryItems.Keys;

            Database.DBProvider.CurrentQueryItem = Database.IDbSchema.DefaultQueryItem;

            foreach (var dbName in dbNames)
            {
                if (_isExists.Contains(dbName))
                    continue;

                if (Database.DBProvider.IsDatabaseExists(dbName))
                {
                    _isExists.Add(dbName);
                    continue;
                }

                Create(context.QueryItems[dbName].First());
                _isExists.Add(dbName);

            }
        }

        public void Execute(string sql)
        {
            Database.DBProvider.DbContext.Sql(sql).Execute();
        }
    }
}
