using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class SaleByDay
    {

        public string SysNo { get; set; }

        public String DataSource { get; set; }

        public String OutProductID { get; set; }

        public string BrandID { get; set; }

        public string CategoryID { get; set; }

        public string ProductID { get; set; }

        public String ProductName { get; set; }

        public string ShopID { get; set; }

        public String ShopName { get; set; }

        public DateTime StatisticalDate { get; set; }

        public Decimal Sales { get; set; }

        public Int32 NumberOfSales { get; set; }

        public Decimal AveragePrice { get; set; }

        public Int32 OrderQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateUserID { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UpdateUserID { get; set; }

        public string ImportGroupId { get; set; }

        public Boolean IsExclude { get; set; }

    }
}
