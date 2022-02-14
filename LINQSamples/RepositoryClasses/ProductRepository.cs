using System.Collections.Generic;
using LINQSamples.EntityClasses;

namespace LINQSamples.RepositoryClasses
{
    public partial class ProductRepository
    {
        public static List<Product> GetAll()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductId = 680,
                    Name = "HL Road Frame",
                    Color = "Black",
                    StandardCost = 1059.31M,
                    ListPrice = 1431.50M,
                    Size = "58"
                }
            };
        }
    }
}