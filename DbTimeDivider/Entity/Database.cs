using DataDepots.Core;
using DbTimeDivider.IFace;
using DbTimeDivider.Schema;
using System.Collections.Generic;
using System.Linq;

namespace DbTimeDivider.Entity
{
    public class Database
    {
        public DbHost DbHost { get; set; }

        public string Port { get; set; }

        public string ConnStr { get; set; }

        public string Name { get; set; }

        public string UID { get; set; }

        public string Password { get; set; }

        public DivisionFlag DivisionFlag { get; set; }

        private Dictionary<string, Table> _tables = null;
        public Dictionary<string, Table> Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = TimeDivider.GetServices<ITableSchema>()
                        .Select(o => o.Table)
                        .Where(o => o.Database == this)
                        .ToDictionary(k => k.Name, v => v);
                }
                return _tables;
            }
        }


        public AbsDBProvider DBProvider { get; set; }

        public IDbSchema IDbSchema { get; set; }
    }
}
