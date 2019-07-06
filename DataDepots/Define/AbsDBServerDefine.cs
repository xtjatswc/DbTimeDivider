using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots
{
    public abstract class AbsDBServerDefine
    {
        public DBServer DBServer { get; set; }

        public AbsDBServerDefine()
        {
            DBServer = new DBServer();
            Define();
        }

        protected abstract void Define();
    }
}
