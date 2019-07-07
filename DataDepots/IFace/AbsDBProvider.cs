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
        protected SingleSql SingleSql { get; set; }

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
                if (_dictDbContext.ContainsKey(SingleSql.DatabaseName))
                {
                    return _dictDbContext[SingleSql.DatabaseName];
                }
                else
                {
                    var dbContext = GetDbContext();
                    _dictDbContext.Add(SingleSql.DatabaseName, dbContext);
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

        public IEnumerable<DataRow> GetTable(ExecContext context)
        {
            IEnumerable<DataRow> result = new List<DataRow>();
            foreach (var singleSql in context.SqlList)
            {
                SingleSql = singleSql;
                var tbl = DbContext.Sql(singleSql.ExecSql).QuerySingle<DataTable>();
                result = result.Concat(tbl.AsEnumerable());
            }

            return result;
        }

    }
}
