using DbTimeDivider.Schema;
using System;
using System.Collections.Generic;

namespace DbTimeDivider.Entity
{
    public class DivisionContext
    {

        public IDbSchema IDbSchema { get; set; }

        public Database Database { get; set; }

        public ITableSchema ITableSchema { get; set; }

        public Table Table { get; set; }

        public List<ParticleSet> Particles { get; set; }

        public DateTime TargetTime1 { get; set; }

        public DateTime TargetTime2 { get; set; }

    }
}
