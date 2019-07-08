using DbTimeDivider;
using DbTimeDivider.Entity;
using DbTimeDivider.Schema;
using FluentData;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbTimeDivider.IFace
{
    public abstract class AbsDBProvider
    {
        public ParticleSet CurrentParticleSet { get; set; }

        private Database _database = null;
        protected Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = TimeDivider.GetServices<IDbSchema>().Select(o => o.Database).First(o => o.DBProvider == this);
                }
                return _database;
            }
        }

        private SortedDictionary<string, IDbContext> _dictDbContext = new SortedDictionary<string, IDbContext>();
        public IDbContext DbContext
        {
            get
            {
                if (_dictDbContext.ContainsKey(CurrentParticleSet.DatabaseName))
                {
                    return _dictDbContext[CurrentParticleSet.DatabaseName];
                }
                else
                {
                    var dbContext = GetDbContext();
                    _dictDbContext.Add(CurrentParticleSet.DatabaseName, dbContext);
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

        public IEnumerable<DataRow> GetTable(DivisionContext context)
        {
            //判断数据库、表是否存在
            context.IDbSchema.CheckExists(context);
            context.ITableSchema.CheckExists(context);

            IEnumerable<DataRow> result = new List<DataRow>();
            foreach (var singleSql in context.Particles)
            {
                CurrentParticleSet = singleSql;
                var tbl = DbContext.Sql(singleSql.ExecSql).QuerySingle<DataTable>();
                result = result.Concat(tbl.AsEnumerable());
            }

            return result;
        }

    }
}
