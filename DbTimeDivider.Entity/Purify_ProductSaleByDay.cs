using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeDivider.Entity
{
    public class Purify_ProductSaleByDay
    {

        public Guid SysNo { get; set; }

        public String DataSource { get; set; }

        public String OutProductID { get; set; }

        public Guid BrandID { get; set; }

        public Guid CategoryID { get; set; }

        public Guid ProductID { get; set; }

        public String ProductName { get; set; }

        public Guid ShopID { get; set; }

        public String ShopName { get; set; }

        public DateTime StatisticalDate { get; set; }

        public Decimal Sales { get; set; }

        public Int32 NumberOfSales { get; set; }

        public Decimal AveragePrice { get; set; }

        public Int32 OrderQuantity { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CreateUserID { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid UpdateUserID { get; set; }

        public Guid ImportGroupId { get; set; }

        public Boolean IsExclude { get; set; }

    }
}
