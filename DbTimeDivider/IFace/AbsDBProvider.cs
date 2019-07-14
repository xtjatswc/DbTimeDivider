﻿using DbTimeDivider.Entity;
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

        public DataTable GetTable(DivisionContext context, params object[] parameters)
        {
            CheckExists(context);

            DataTable retTbl = null;
            foreach (var queryItem in context.QueryItems)
            {
                CurrentQueryItem = queryItem;
                var tbl = DbContext.Sql(queryItem.ExecSql, parameters).QuerySingle<DataTable>();
                if(retTbl == null)
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

        public List<T> GetList<T>(DivisionContext context, params object[] parameters)
        {
            CheckExists(context);

            List<T> retList = new List<T>();
            foreach (var queryItem in context.QueryItems)
            {
                CurrentQueryItem = queryItem;
                var list = DbContext.Sql(queryItem.ExecSql, parameters).QueryMany<T>();
                retList.AddRange(list);
            }

            return retList;
        }
    }
}
