using Autofac;
using DataDepots.Util;
using DbTimeDivider.Config;
using DbTimeDivider.Entity;
using DbTimeDivider.Schema;
using System.Collections.Generic;
using System.Linq;

namespace DbTimeDivider
{
    public class TimeDivider
    {
        public static IContainer iContainer;

        private static Dictionary<string, DbHost> _dbHosts = null;
        public static Dictionary<string, DbHost> DbHosts
        {
            get
            {
                if (_dbHosts == null)
                {
                    _dbHosts = TimeDivider.GetServices<AbsDbHostSchema>()
                        .Select(o => o.DbHost)
                        .ToDictionary(k => k.IP, v => v);
                }
                return _dbHosts;
            }
        }

        public static void Register(string assemblyString)
        {
            if (iContainer == null)
                iContainer = AutofacConfig.Register(assemblyString);
        }

        public static TService GetService<TService>()
        {
            return iContainer.GetService<TService>();
        }

        public static IEnumerable<TService> GetServices<TService>()
        {
            return iContainer.GetServices<TService>();
        }

    }
}
