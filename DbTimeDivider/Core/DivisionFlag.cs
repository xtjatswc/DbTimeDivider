using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepots.Core
{
    public enum DivisionFlag
    {
        //年
        yyyy,
        yy,
        //月
        MM,
        yyyyMM,
        yyMM,
        //日
        dd,
        MMdd,
        yyyyMMdd,
        yyMMdd,
        //小时
        HH,
        ddHH,
        MMddHH,
        yyyyMMddHH,
        yyMMddHH
    }
}
