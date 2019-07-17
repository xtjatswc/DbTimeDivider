using FluentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class CacheDbContext
    {
        public IDbContext DbContext { get; set; }

        public DateTime CacheTime { get; set; }

    }
}
