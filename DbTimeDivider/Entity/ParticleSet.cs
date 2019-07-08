using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class ParticleSet
    {
        public string DatabaseName { get; set; }

        public string TableName { get; set; }

        public string ExecSql { get; set; }

        public DateTime TargetTime { get; set; }

    }
}
