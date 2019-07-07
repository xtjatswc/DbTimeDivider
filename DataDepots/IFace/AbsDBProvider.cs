using DataDepots.Define;
using FluentData;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataDepots
{
    public abstract class AbsDBProvider
    {
        private Database _database = null;
        protected Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Depots.GetServices<IDatabaseDefine>().Select(o => o.Database).First(o => o.DBProvider == this);
                }
                return _database;
            }
        }

        private SortedDictionary<string, IDbContext> _dictDbContext = new SortedDictionary<string, IDbContext>();
        protected IDbContext DbContext
        {
            get
            {
                if (_dictDbContext.ContainsKey(Database.Name2))
                {
                    return _dictDbContext[Database.Name2];
                }
                else
                {
                    var dbContext = GetDbContext();
                    _dictDbContext.Add(Database.Name2, dbContext);
                    return dbContext;
                }
            }
        }

        protected abstract string GetConnStr();

        protected abstract IDbProvider GetDbProvider();

        private IDbContext GetDbContext()
        {
            return new DbContext().ConnectionString(GetConnStr(), GetDbProvider());
        }

        public abstract DataTable GetTable(string sql);

    }
}
