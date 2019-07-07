using DataDepots.Define;
using DataDepots.Util;
using System.Collections.Generic;
using System.Linq;

namespace DataDepots
{
    public class DBServer
    {
        public string IP { get; set; }

        private Dictionary<string, Database> _databases;
        public Dictionary<string, Database> Databases
        {
            get
            {
                if (_databases == null)
                {
                    _databases = Depots.iContainer.GetServices<IDataBaseDefine>()
                        .Select(o => o.Database)
                        .Where(o => o.DBServer == this)
                        .ToDictionary(k => k.Name, v => v);
                }
                return _databases;
            }
        }
    }
}
