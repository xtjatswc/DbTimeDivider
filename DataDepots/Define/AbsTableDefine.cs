using DataDepots.Define;
using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots.IFace
{
    public abstract class AbsTableDefine<BelongToClass> : ITableDefine
    {
        public Table Table { get; set; }

        public AbsTableDefine()
        {
            Table = new Table
            {
                Database = (Depots.iContainer.GetService<BelongToClass>() as IDataBaseDefine).Database,
            };
            Define();
        }

        protected abstract void Define();
    }
}
