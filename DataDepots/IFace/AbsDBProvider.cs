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
        public SingleSql SingleSql { get; set; }

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
        public IDbContext DbContext
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

        public abstract bool IsDatabaseExists(string dbName);

        public abstract bool IsTableExists(string tableName);

        private IDbContext GetDbContext()
        {
            return new DbContext().ConnectionString(GetConnStr(), GetDbProvider());
        }

        public IEnumerable<DataRow> GetTable(ExecContext context)
        {
            //判断数据库、表是否存在
            context.IDatabaseDefine.CheckExists(context);
            context.ITableDefine.CheckExists(context);

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
