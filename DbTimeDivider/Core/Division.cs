using DataDepots.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Core
{
    public static class Division
    {
        public static DivisionType GetDivisionType(this DivisionFlag divisionFlag)
        {
            switch (divisionFlag)
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

        public static void SetTargetTime(this DivisionType divisionType, ref DateTime targetTime1, ref DateTime targetTime2)
        {
            switch (divisionType)
            {
                case DivisionType.Year:
                    targetTime1 = DateTime.Parse(targetTime1.ToString("yyyy-01-01"));
                    targetTime2 = DateTime.Parse(targetTime2.ToString("yyyy-01-01")).AddYears(1).AddSeconds(-1);
                    break;
                case DivisionType.Month:
                    targetTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-01"));
                    targetTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-01")).AddMonths(1).AddSeconds(-1);
                    break;
                case DivisionType.Day:
                    targetTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd"));
                    targetTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd")).AddDays(1).AddSeconds(-1);
                    break;
                case DivisionType.Hour:
                    targetTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd HH:00:00"));
                    targetTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd HH:00:00")).AddHours(1).AddSeconds(-1);
                    break;
                default:
                    break;
            }

        }

        public static void PlusTargetTime(this DivisionType divisionType, ref DateTime targetTime)
        {
            switch (divisionType)
            {
                case DivisionType.Year:
                    targetTime = targetTime.AddYears(1);
                    break;
                case DivisionType.Month:
                    targetTime = targetTime.AddMonths(1);
                    break;
                case DivisionType.Day:
                    targetTime = targetTime.AddDays(1);
                    break;
                case DivisionType.Hour:
                    targetTime = targetTime.AddHours(1);
                    break;
                default:
                    break;
            }

        }
    }
}
