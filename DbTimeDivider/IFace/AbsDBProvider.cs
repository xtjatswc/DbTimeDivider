﻿using DbTimeDivider.Entity;
using DbTimeDivider.Schema;
using FluentData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace DbTimeDivider.IFace
{
    public abstract class AbsDBProvider
    {
        private static object _lockObj = new object();

        public SortedDictionary<string, QueryItem> _currentQueryItem = new SortedDictionary<string, QueryItem>();
        public QueryItem CurrentQueryItem
        {
            get
            {
                lock (_lockObj)
                {
                    string key = $"ThreadId [{Thread.CurrentThread.ManagedThreadId}]";
                    return _currentQueryItem[key];
                }
            }
            set
            {
                lock (_lockObj)
                {
                    string key = $"ThreadId [{Thread.CurrentThread.ManagedThreadId}]";
                    if (_currentQueryItem.ContainsKey(key))
                    {
                        _currentQueryItem[key] = value;
                    }
                    else
                    {
                        _currentQueryItem.Add(key, value);
                    }
                }
            }
        }

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
                lock (_lockObj)
                {
                    string key = $"DatabaseName [{CurrentQueryItem.DatabaseName}] && ThreadId [{Thread.CurrentThread.ManagedThreadId}]";
                    if (_dictDbContext.ContainsKey(key))
                    {
                        return _dictDbContext[key];
                    }
                    else
                    {
                        var dbContext = GetDbContext();
                        _dictDbContext.Add(key, dbContext);
                        return dbContext;
                    }
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
            foreach (var dbQueryItem in context.QueryItems)
            {
                CurrentQueryItem = new QueryItem { DatabaseName = dbQueryItem.Key };
                foreach (var queryItem in dbQueryItem.Value)
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
            }

            return retTbl;
        }

        public List<T> GetList<T>(DivisionContext context)
        {
            CheckExists(context);

            List<T> retList = new List<T>();
            foreach (var dbQueryItem in context.QueryItems)
            {
                CurrentQueryItem = new QueryItem { DatabaseName = dbQueryItem.Key };
                foreach (var queryItem in dbQueryItem.Value)
                {
                    CurrentQueryItem = queryItem;
                    var list = GetDbCommand(DbContext, context.QueryPara, queryItem).QueryMany<T>();
                    retList.AddRange(list);
                }
            }

            return retList;
        }

        public int Execute(DivisionContext context)
        {
            CheckExists(context);

            int affectedRows = 0;
            foreach (var dbQueryItem in context.QueryItems)
            {
                CurrentQueryItem = new QueryItem { DatabaseName = dbQueryItem.Key };
                using (var dbContext = DbContext.UseTransaction(context.QueryPara.UseTransaction))
                {
                    foreach (var queryItem in dbQueryItem.Value)
                    {
                        CurrentQueryItem = queryItem;
                        affectedRows += GetDbCommand(dbContext, context.QueryPara, queryItem).Execute();
                    }
                    dbContext.Commit();
                }
            }

            return affectedRows;
        }

        public int Insert<T>(DivisionContext context, T t, params Expression<Func<T, object>>[] ignoreProperties)
        {
            CheckExists(context);

            int affectedRows = 0;
            foreach (var dbQueryItem in context.QueryItems)
            {
                CurrentQueryItem = new QueryItem { DatabaseName = dbQueryItem.Key };
                using (var dbContext = DbContext.UseTransaction(context.QueryPara.UseTransaction))
                {
                    foreach (var queryItem in dbQueryItem.Value)
                    {
                        CurrentQueryItem = queryItem;
                        affectedRows += dbContext.Insert<T>(queryItem.TableNames.First().Value, t)
                            .AutoMap(ignoreProperties)
                            .Execute();
                    }
                    dbContext.Commit();
                }
            }

            return affectedRows;
        }

        public int Update<T>(DivisionContext context, T t, Expression<Func<T, object>> whereProperties, params Expression<Func<T, object>>[] ignoreProperties)
        {
            CheckExists(context);

            int affectedRows = 0;
            foreach (var dbQueryItem in context.QueryItems)
            {
                CurrentQueryItem = new QueryItem { DatabaseName = dbQueryItem.Key };
                using (var dbContext = DbContext.UseTransaction(context.QueryPara.UseTransaction))
                {
                    foreach (var queryItem in dbQueryItem.Value)
                    {
                        CurrentQueryItem = queryItem;
                        affectedRows += dbContext.Update<T>(queryItem.TableNames.First().Value, t)
                            .AutoMap(ignoreProperties)
                            .Where(whereProperties)
                            .Execute();
                    }
                    dbContext.Commit();
                }
            }

            return affectedRows;
        }

    }
}
