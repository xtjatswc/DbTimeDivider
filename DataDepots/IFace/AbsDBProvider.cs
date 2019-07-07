using DataDepots.Define;
using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataDepots
{
    public abstract class AbsDBProvider
    {
        private Database _database = null;
        public Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Depots.GetServices<IDatabaseDefine>().Select(o => o.Database).First(o => o.DBProvider == this);
                }
                return _database;
            }
        }

        public abstract DataTable GetTable(string sql);
    }
}
