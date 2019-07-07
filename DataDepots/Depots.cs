using Autofac;
using DataDepots.IFace;
using DataDepots.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDepots
{
    public class Depots
    {
        public static IContainer iContainer;

        private static Dictionary<string, Server> _servers = null;
        public static Dictionary<string, Server> Servers
        {
            get
            {
                if (_servers == null)
                {
                    _servers = Depots.iContainer.GetServices<AbsServerDefine>()
                        .Select(o => o.Server)
                        .ToDictionary(k => k.IP, v => v);
                }
                return _servers;
            }
        }


        public static void Register(string assemblyString)
        {
            iContainer = AutofacConfig.Register(assemblyString);
        }


    }
}
