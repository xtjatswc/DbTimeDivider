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

        public static Dictionary<string, DBServer> DBServer = new Dictionary<string, DBServer>();

        public static void Register(string assemblyString)
        {
            iContainer = AutofacConfig.Register(assemblyString);

            //var svrDefines = iContainer.GetServices<AbsDBServerDefine>();

            //foreach (var svrDefine in svrDefines)
            //{
            //    var dbServer = svrDefine.DBServer;
            //    DBServer.Add(dbServer.IP, dbServer);
            //    dbServer.AddDataBase();
            //}
        }


    }
}
