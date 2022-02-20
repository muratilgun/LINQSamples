using System.Collections;
using System.Collections.Generic;

namespace LINQSamples.EntityClasses
{
    public partial class ProductSales
    {
        public Product Product { get; set; }
        public IEnumerable<SalesOrderDetail> Sales { get; set; }

    }
}