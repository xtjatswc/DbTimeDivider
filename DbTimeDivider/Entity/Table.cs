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

        public IEnumerable<DataRow> Query(string sql, DateTime targetTime1)
        {
            return Query(sql, targetTime1, targetTime1);
        }

        public IEnumerable<DataRow> Query(string sql, DateTime targetTime1, DateTime targetTime2)
        {
            DivisionContext context = new DivisionContext();
            context.IDbSchema = this.Database.IDbSchema;
            context.Database = this.Database;
            context.ITableSchema = this.ITableSchema;
            context.Table = this;
            context.TargetTime1 = targetTime1;
            context.TargetTime2 = targetTime2;
            context.Particles = new List<ParticleSet>();

            DateTime tempTime1 = targetTime1;
            DateTime tempTime2 = targetTime2;

            switch (DivisionType)
            {
                case DivisionType.Year:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-01-01"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-01-01")).AddYears(1).AddSeconds(-1);
                    break;
                case DivisionType.Month:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-01"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-01")).AddMonths(1).AddSeconds(-1);
                    break;
                case DivisionType.Day:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd")).AddDays(1).AddSeconds(-1);
                    break;
                case DivisionType.Hour:
                    tempTime1 = DateTime.Parse(targetTime1.ToString("yyyy-MM-dd HH:00:00"));
                    tempTime2 = DateTime.Parse(targetTime2.ToString("yyyy-MM-dd HH:00:00")).AddHours(1).AddSeconds(-1);
                    break;
                default:
                    break;
            }

            while (true)
            {
                ParticleSet particle = new ParticleSet();
                particle.DatabaseName = string.Format(Database.Name, tempTime1.ToString(Enum.GetName(typeof(DivisionFlag), Database.DivisionFlag) ));
                particle.TableName = string.Format(Name, tempTime1.ToString(Enum.GetName(typeof(DivisionFlag), DivisionFlag)));
                particle.ExecSql = string.Format(sql, particle.TableName);
                particle.TargetTime = tempTime1;

                context.Particles.Add(particle);

                switch (DivisionType)
                {
                    case DivisionType.Year:
                        tempTime1 = tempTime1.AddYears(1);
                        break;
                    case DivisionType.Month:
                        tempTime1 = tempTime1.AddMonths(1);
                        break;
                    case DivisionType.Day:
                        tempTime1 = tempTime1.AddDays(1);
                        break;
                    case DivisionType.Hour:
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
