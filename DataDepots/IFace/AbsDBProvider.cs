using DataDepots.Define;
using DataDepots.Entity;
using FluentData;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataDepots
{
    public abstract class AbsDBProvider
    {
        protected ExecContext ExecContext { get; set; }

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
                if (_dictDbContext.ContainsKey(ExecContext.DatabaseName))
                {
                    return _dictDbContext[ExecContext.DatabaseName];
                }
                else
                {
                    var dbContext = GetDbContext();
                    _dictDbContext.Add(ExecContext.DatabaseName, dbContext);
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

        public DataTable GetTable(ExecContext context)
        {
            ExecContext = context;
            var tbl = DbContext.Sql(context.ExecSql).QuerySingle<DataTable>();
            return tbl;
        }

    }
}
