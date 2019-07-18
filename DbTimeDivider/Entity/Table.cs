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

        public string GetRealName(DateTime targetTime)
        {
            if(DivisionType == DivisionType.None)
            {
                return Name;
            }
            else
            {
                return string.Format(Name, targetTime.ToString(Enum.GetName(typeof(DivisionFlag), DivisionFlag)));
            }
        }

    }
}
