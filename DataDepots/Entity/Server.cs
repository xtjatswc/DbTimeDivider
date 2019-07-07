using DataDepots.Define;
using DataDepots.Util;
using System.Collections.Generic;
using System.Linq;

namespace DataDepots
{
    public class Server
    {
        public string IP { get; set; }

        private Dictionary<string, Database> _databases;
        public Dictionary<string, Database> Databases
        {
            get
            {
                if (_databases == null)
                {
                    _databases = Depots.iContainer.GetServices<IDatabaseDefine>()
                        .Select(o => o.Database)
                        .Where(o => o.DBServer == this)
                        .ToDictionary(k => k.Name, v => v);
                }
                return _databases;
            }
        }
    }
}
