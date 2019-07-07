﻿using DataDepots;
using DataDepots.Define;
using DataDepots.Core;

namespace DataDepotsDemo
{
    public class Lnsky_Test : AbsDatabaseDefine<Server1, SqlServerDBProvider>
    {

        protected override void Define()
        {
            Database.Name = "Lnsky_Test_{0}";
            Database.UID = "sa";
            Database.Password = "sa";
            Database.DepotsFlag = DepotsFlag.yy;
        }

    }
}
