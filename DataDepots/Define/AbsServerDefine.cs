using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots
{
    public abstract class AbsServerDefine
    {
        public Server Server { get; set; }

        public AbsServerDefine()
        {
            Server = new Server();
            Define();
        }

        protected abstract void Define();
    }
}
