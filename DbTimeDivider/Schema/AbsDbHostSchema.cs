using DbTimeDivider.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbTimeDivider.Schema
{
    public abstract class AbsDbHostSchema
    {
        public DbHost DbHost { get; set; }

        public AbsDbHostSchema()
        {
            DbHost = new DbHost();
            Define();
        }

        protected abstract void Define();
    }
}
