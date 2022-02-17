using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LINQSamples.EntityClasses;

namespace LINQSamples.RepositoryClasses
{
    public static class ProductHelper
    {
        public static IEnumerable<Product> ByColor(this IEnumerable<Product> query, string color)
        {
            return query.Where(prod => prod.Color == color);
        }
    }
}