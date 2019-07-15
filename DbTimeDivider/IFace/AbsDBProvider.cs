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
        public QueryItem CurrentQueryItem { get; set; }

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
                if (_dictDbContext.ContainsKey(CurrentQueryItem.DatabaseName))
                {
                    return _dictDbContext[CurrentQueryItem.DatabaseName];
                }
                else
                {
                    var dbContext = GetDbContext();
                    _dictDbContext.Add(CurrentQueryItem.DatabaseName, dbContext);
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

        private void CheckExists(DivisionContext context)
        {
            //判断数据库、表是否存在
            context.IDbSchema.CheckExists(context);
            foreach (var schema in context.ITableSchemas)
            {
                schema.CheckExists(context);
            }
        }

        private FluentData.IDbCommand GetDbCommand(IDbContext dbContext, QueryPara queryPara, QueryItem queryItem)
        {
            FluentData.IDbCommand dbCommand = dbContext.Sql(queryItem.ExecSql, queryPara.Parameters.ToArray());
            foreach (var param in queryPara.ParamSet)
            {
                dbCommand = dbCommand.Parameter(param.Key, param.Value);
            }

            return dbCommand;
        }

        public DataTable GetTable(DivisionContext context)
        {
            CheckExists(context);

            DataTable retTbl = null;
            foreach (var queryItem in context.QueryItems)
            {
                CurrentQueryItem = queryItem;

                var tbl = GetDbCommand(DbContext, context.QueryPara, queryItem).QuerySingle<DataTable>();
                if (retTbl == null)
                {
                    retTbl = tbl;
                }
                else
                {
                    retTbl.Merge(tbl);
                }
            }

            return retTbl;
        }

        public List<T> GetList<T>(DivisionContext context)
        {
            CheckExists(context);

            List<T> retList = new List<T>();
            foreach (var queryItem in context.QueryItems)
            {
                CurrentQueryItem = queryItem;

                var list = GetDbCommand(DbContext, context.QueryPara, queryItem).QueryMany<T>();
                retList.AddRange(list);
            }

            return retList;
        }

        public int Execute(DivisionContext context)
        {
            CheckExists(context);

            int affectedRows = 0;
            foreach (var queryItem in context.QueryItems)
            {
                CurrentQueryItem = queryItem;

                using (var dbContext = DbContext.UseTransaction(context.QueryPara.UseTransaction))
                {
                    affectedRows += GetDbCommand(dbContext, context.QueryPara, queryItem).Execute();
                }
            }

            return affectedRows;
        }
    }
}
