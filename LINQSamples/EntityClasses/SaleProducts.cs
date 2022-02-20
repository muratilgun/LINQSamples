using System.Collections.Generic;

namespace LINQSamples.EntityClasses
{
    public partial class SaleProducts
    {
        public int SalesOrderID { get; set; }
        public List<Product> Products { get; set; }
    }
}