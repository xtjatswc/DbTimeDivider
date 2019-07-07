using DataDepots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepotsDemo
{
    public class Server1 : AbsServerDefine
    {
        protected override void Define()
        {
            Server.IP = ".";
        }
    }
}
