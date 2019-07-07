using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots.Define
{
    public abstract class AbsDataBaseDefine<BelongToClass, DBProviderClass>: IDataBaseDefine
    {
        public Type BelongTo => typeof(BelongToClass);

        public Database Database { get; set; }

        public AbsDataBaseDefine()
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
