using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class OracleSaleByDay
    {

        public String SYSNO { get; set; }

        public String DATASOURCE { get; set; }

        public String OUTPRODUCTID { get; set; }

        public String BRANDID { get; set; }

        public String CATEGORYID { get; set; }

        public String PRODUCTID { get; set; }

        public String PRODUCTNAME { get; set; }

        public String SHOPID { get; set; }

        public String SHOPNAME { get; set; }

        public DateTime STATISTICALDATE { get; set; }

        public Decimal SALES { get; set; }

        public Int64 NUMBEROFSALES { get; set; }

        public Decimal AVERAGEPRICE { get; set; }

        public Int64 ORDERQUANTITY { get; set; }

        public DateTime CREATEDATE { get; set; }

        public String CREATEUSERID { get; set; }

        public DateTime UPDATEDATE { get; set; }

        public String UPDATEUSERID { get; set; }

        public String IMPORTGROUPID { get; set; }

        public Int16 ISEXCLUDE { get; set; }

    }
}
