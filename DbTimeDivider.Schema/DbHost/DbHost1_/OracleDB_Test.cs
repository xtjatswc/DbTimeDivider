using DbTimeDivider.Core;
using DbTimeDivider.Entity;
using DbTimeDivider.Schema.DBProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Schema.DbHost.DbHost1_
{
    public class OracleDB_Test : AbsDbSchema<DbHost1, OracleDBProvider>
    {
        public override void Create(QueryItem queryItem)
        {
            string sql = string.Format(@"
--创建表空间
create tablespace ts{0}
datafile 'D:\app\Administrator\oradata\test\ts{0}.dbf' size 10m
autoextend on
next 10m
maxsize unlimited
extent management local;

--创建用户
create user user{0}
identified by pwd2019
default tablespace ts{0};

--为用户赋权限
grant exp_full_database,imp_full_database,dba,connect,resource,create session,create any sequence,
create any trigger to user{0}
", queryItem.DatabaseSuffix);

            SplitExecute(sql, ";\r\n");
        }

        protected override void Define()
        {
            Database.Name = "user{0}";
            Database.UID = "orcl";
            Database.Password = "pwd2019";
            Database.Port = "1521";
            Database.DivisionFlag = DivisionFlag.yyyy;
        }

        protected override QueryItem GetDefaultQueryItem()
        {
            return new QueryItem { DatabaseName = "user2019" };
        }
    }
}
