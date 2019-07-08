using DataDepots.Define;
using DataDepots.Core;
using DataDepots.Util;
using System.Collections.Generic;
using System.Linq;

namespace DataDepots
{
    public class Database
    {
        public Server DBServer { get; set; }

        public string Port { get; set; }

        public string ConnStr { get; set; }

        public string Name { get; set; }

        public string UID { get; set; }

        public string Password { get; set; }

        public DepotsFlag DepotsFlag { get; set; }

        private Dictionary<string, Table> _tables = null;
        public Dictionary<string, Table> Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = Depots.GetServices<ITableDefine>()
                        .Select(o => o.Table)
                        .Where(o => o.Database == this)
                        .ToDictionary(k => k.Name, v => v);
                }
                return _tables;
            }
        }


        public AbsDBProvider DBProvider { get; set; }

        public IDatabaseDefine IDatabaseDefine { get; set; }
    }
}
