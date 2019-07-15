using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class QueryPara
    {
        public string Sql { get; set; }

        private DateTime? _targetTime1;
        public DateTime TargetTime1
        {
            get
            {
                if (_targetTime1 == null)
                    _targetTime1 = DateTime.Now;

                return _targetTime1.Value;
            }
            set
            {
                _targetTime1 = value;
            }
        }

        private DateTime? _targetTime2;
        public DateTime TargetTime2
        {
            get
            {
                if (_targetTime2 == null)
                    _targetTime2 = TargetTime1;

                return _targetTime2.Value;
            }
            set
            {
                _targetTime2 = value;
            }
        }

        public Dictionary<string, object> ParamSet { get; set; } = new Dictionary<string, object>();

        public List<object> Parameters { get; set; } = new List<object>();

        public bool UseTransaction { get; set; }
    }
}
