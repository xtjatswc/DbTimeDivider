using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class CacheQueryItem
    {
        public QueryItem QueryItem { get; set; }

        public DateTime CacheTime { get; set; }
    }
}
