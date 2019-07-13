using DataDepots.Core;
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
                switch (DivisionFlag)
                {
                    case DivisionFlag.None:
                        return DivisionType.None;
                    case DivisionFlag.yyyy:
                    case DivisionFlag.yy:
                        return DivisionType.Year;
                    case DivisionFlag.MM:
                    case DivisionFlag.yyyyMM:
                    case DivisionFlag.yyMM:
                        return DivisionType.Month;
                    case DivisionFlag.dd:
                    case DivisionFlag.MMdd:
                    case DivisionFlag.yyyyMMdd:
                    case DivisionFlag.yyMMdd:
                        return DivisionType.Day;
                    case DivisionFlag.HH:
                    case DivisionFlag.ddHH:
                    case DivisionFlag.MMddHH:
                    case DivisionFlag.yyyyMMddHH:
                    case DivisionFlag.yyMMddHH:
                        return DivisionType.Hour;
                    default:
                        throw new Exception("未识别的DepotsFlag");
                }
            }
        }

    }
}
