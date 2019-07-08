using DataDepots.Core;
using DataDepots.Define;
using DataDepots.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataDepots
{
    public class Table
    {
        public Database Database { get; set; }

        public string Name { get; set; }

        public DepotsFlag DepotsFlag { get; set; }

        public ITableDefine ITableDefine { get; set; }

        public DepotsType DepotsType
        {
            get
            {
                switch (DepotsFlag)
                {
                    case DepotsFlag.yyyy:
                    case DepotsFlag.yy:
                        return DepotsType.Year;
                    case DepotsFlag.MM:
                    case DepotsFlag.yyyyMM:
                    case DepotsFlag.yyMM:
                        return DepotsType.Month;
                    case DepotsFlag.dd:
                    case DepotsFlag.MMdd:
                    case DepotsFlag.yyyyMMdd:
                    case DepotsFlag.yyMMdd:
                        return DepotsType.Day;
                    case DepotsFlag.HH:
                    case DepotsFlag.ddHH:
                    case DepotsFlag.MMddHH:
                    case DepotsFlag.yyyyMMddHH:
                    case DepotsFlag.yyMMddHH:
                        return DepotsType.Hour;
                    default:
                        throw new Exception("未识别的DepotsFlag");
                }
            }
        }

        public IEnumerable<DataRow> Query(string sql, DateTime targetTime1)
        {
            return Query(sql, targetTime1, targetTime1);
        }

        public IEnumerable<DataRow> Query(string sql, DateTime targetTime1, DateTime targetTime2)
        {
            ExecContext context = new ExecContext();
            context.IDatabaseDefine = this.Database.IDatabaseDefine;
            context.Database = this.Database;
            context.ITableDefine = this.ITableDefine;
            context.Table = this;
            context.TargetTime1 = targetTime1;
            context.TargetTime2 = targetTime2;
            context.SqlList = new List<SingleSql>();

            DateTime tempTime1 = targetTime1;
            DateTime tempTime2 = targetTime2;

            switch (DepotsType)
            {
                case DepotsType.Year:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-01-01"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-01-01")).AddYears(1).AddSeconds(-1);
                    break;
                case DepotsType.Month:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-01"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-01")).AddMonths(1).AddSeconds(-1);
                    break;
                case DepotsType.Day:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd")).AddDays(1).AddSeconds(-1);
                    break;
                case DepotsType.Hour:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd HH:00:00"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd HH:00:00")).AddHours(1).AddSeconds(-1);
                    break;
                default:
                    break;
            }

            while (true)
            {
                SingleSql singleSql = new SingleSql();
                singleSql.DatabaseName = string.Format(Database.Name, tempTime1.ToString(Enum.GetName(typeof(DepotsFlag), Database.DepotsFlag) ));
                singleSql.TableName = string.Format(Name, tempTime1.ToString(Enum.GetName(typeof(DepotsFlag), DepotsFlag)));
                singleSql.ExecSql = string.Format(sql, singleSql.TableName);
                singleSql.TargetTime = tempTime1;

                context.SqlList.Add(singleSql);

                switch (DepotsType)
                {
                    case DepotsType.Year:
                        tempTime1 = tempTime1.AddYears(1);
                        break;
                    case DepotsType.Month:
                        tempTime1 = tempTime1.AddMonths(1);
                        break;
                    case DepotsType.Day:
                        tempTime1 = tempTime1.AddDays(1);
                        break;
                    case DepotsType.Hour:
                        tempTime1 = tempTime1.AddHours(1);
                        break;
                    default:
                        break;
                }

                if(tempTime1 > tempTime2)
                {
                    break;
                }
            }

            return Database.DBProvider.GetTable(context);
        }

    }
}
