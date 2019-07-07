using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots.Define
{
    public abstract class AbsDatabaseDefine<BelongToClass, DBProviderClass>: IDatabaseDefine
    {
        public Type BelongTo => typeof(BelongToClass);

        public Database Database { get; set; }

        public AbsDatabaseDefine()
        {
            Database = new Database
            {
                DBServer = (Depots.iContainer.GetService<BelongToClass>() as AbsServerDefine).Server,
                DBProvider = Depots.iContainer.GetService<DBProviderClass>() as IDBProvider
            };
            Define();
        }

        protected abstract void Define();
    }
}
