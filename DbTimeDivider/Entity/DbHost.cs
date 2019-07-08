using DbTimeDivider.Schema;
using System.Collections.Generic;
using System.Linq;

namespace DbTimeDivider.Entity
{
    public class DbHost
    {
        public string IP { get; set; }

        private Dictionary<string, Database> _databases;
        public Dictionary<string, Database> Databases
        {
            get
            {
                if (_databases == null)
                {
                    _databases = TimeDivider.GetServices<IDbSchema>()
                        .Select(o => o.Database)
                        .Where(o => o.DbHost == this)
                        .ToDictionary(k => k.Name, v => v);
                }
                return _databases;
            }
        }
    }
}
