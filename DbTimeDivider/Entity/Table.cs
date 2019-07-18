using DbTimeDivider.Core;
using DbTimeDivider.Schema;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbTimeDivider.Entity
{
    public class Table
    {
        public Database Database { get; set; }

        public string Name { get; set; }

        public DivisionFlag DivisionFlag { get; set; }

        public ITableSchema ITableSchema { get; set; }

        public DivisionType DivisionType
        {
            get
            {
                return DivisionFlag.GetDivisionType();
            }
        }

        public string GetRealName(DateTime targetTime, ref string suffix)
        {
            if(DivisionType == DivisionType.None)
            {
                return Name;
            }
            else
            {
                suffix = targetTime.ToString(Enum.GetName(typeof(DivisionFlag), DivisionFlag));
                return string.Format(Name, suffix);
            }
        }

    }
}
