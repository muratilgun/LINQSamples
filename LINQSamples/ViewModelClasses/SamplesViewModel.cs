using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQSamples.EntityClasses;
using LINQSamples.RepositoryClasses;

namespace LINQSamples.ViewModelClasses
{
    public class SamplesViewModel
    {
        public SamplesViewModel()
        {
            Products = ProductRepository.GetAll();
            Sales = SalesOrderDetailRepository.GetAll();
        }

        public bool UseQuerySyntax = true;
        public List<Product> Products { get; set; }
        public List<SalesOrderDetail> Sales { get; set; }
        public string ResultText { get; set; }

        public void GetAllLooping()
        {
            List<Product> list = new List<Product>();
            foreach (Product item in Products)
            {
                list.Add(item);
            }
            ResultText = $"Total Products: {list.Count}";

        }
        public void GetAll()
        {
            List<Product> list;
            if (UseQuerySyntax)
            {
                // Query Syntax
                list = (from prod in Products select prod).ToList();
            }
            else
            {
                //Method Syntax
                list = Products.Select(prod => prod).ToList();
            }

            ResultText = $"Total Products: {list.Count}";
        }
        public void GetSingleColumn()
        {
            StringBuilder sb = new StringBuilder(1024);
            List<string> list = new List<string>();
            if (UseQuerySyntax)
            {
                list.AddRange(from prod in Products select  prod.Name);
            }
            else
            {
                list.AddRange(Products.Select(prod => prod.Name));
            }

            foreach (string item in list)
            {
                sb.AppendLine(item);
            }

            ResultText = $"Total Products: {list.Count}" + Environment.NewLine + sb.ToString();
            Products.Clear();
            
        }
        public void GetSpecificColumns()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                    select new Product
                    {
                        ProductID = prod.ProductID,
                        Name = prod.Name,
                        Size = prod.Size
                    }).ToList();
            }
            else
            {
                Products = Products.Select(prod => new Product
                {
                    ProductID = prod.ProductID,
                    Name = prod.Name,
                    Size = prod.Size
                }).ToList();
            }
        }
        public void AnonymousClass()
        {
            StringBuilder sb = new StringBuilder(2048);
            if (UseQuerySyntax)
            {
                var products = (from prod in Products
                    select new
                    {
                        Identifier = prod.ProductID,
                        ProductName = prod.Name,
                        ProductSize = prod.Size
                    });

                foreach (var prod in products)
                {
                    sb.AppendLine($"Product ID: {prod.Identifier}");
                    sb.AppendLine($"   Product Name: {prod.ProductName}");
                    sb.AppendLine($"   Product Size: {prod.ProductSize}");
                }
            }
            else
            {
                var products = Products.Select(prod => new
                {
                    Identifier = prod.ProductID,
                    ProductName = prod.Name,
                    ProductSize =prod.Size
                });
                foreach (var prod in products)
                {
                    sb.AppendLine($"Product ID: {prod.Identifier}");
                    sb.AppendLine($"   Product Name: {prod.ProductName}");
                    sb.AppendLine($"   Product Size: {prod.ProductSize}");
                }
            }

            ResultText = sb.ToString();
            Products.Clear();
        }
        public void OrderBy()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Name select prod).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void OrderByDescending()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Name descending select prod).ToList();
            }
            else
            {
                Products = Products.OrderByDescending(prod => prod.Name).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void OrderByTwoFields()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Color descending, prod.Name select prod).ToList();

            }
            else
            {
                Products = Products.OrderByDescending(prod => prod.Color).ThenBy(prod => prod.Name).ToList();

            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void WhereExpression()
        {
            string search = "L";
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                    where prod.Name.StartsWith(search)
                    select prod).ToList();
            }
            else
            {
                Products = Products.Where(prod => prod.Name.StartsWith(search)).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void WhereTwoFields()
        {
            string search = "L";
            decimal cost = 100;
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                    where prod.Name.StartsWith(search) && prod.StandardCost > cost
                    select prod).ToList();
            }
            else
            {
                Products = Products.Where(prod => prod.Name.StartsWith(search) && prod.StandardCost > cost).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void WhereExtensionMethod()
        {
            string search = "Red";
            if (UseQuerySyntax)
            {
                Products = (from prod in Products select prod).ByColor(search).ToList();
            }
            else
            {
                Products = Products.ByColor(search).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";

        }
        public void First()
        {
            string search = "Red";
            Product value;
            try
            {
                if (UseQuerySyntax)
                {
                    value = (from prod in Products select prod).First(prod => prod.Color == search);
                }
                else
                {
                    value = Products.First(prod => prod.Color == search);
                }

                ResultText = $"Found: {value}";
            }
            catch
            {
                ResultText = "Not Found";
            }
            Products.Clear();
        }
        public void FirstOrDefault()
        {
            string search = "Red";
            Product value;
            if (UseQuerySyntax)
            {
                value = (from prod in Products select prod).FirstOrDefault(prod => prod.Color == search);
            }
            else
            {
                value = Products.FirstOrDefault(prod => prod.Color == search);
            }

            if (value == null)
            {
                ResultText = "Not Found";
            }
            else
            {
                ResultText = $"Found : {value}";
            }
            Products.Clear();
        }
        public void Last()
        {
            string search = "Red";
            Product value;
            try
            {
                if (UseQuerySyntax)
                {
                    value = (from prod in Products select prod).Last(prod => prod.Color == search);
                }
                else
                {
                    value = Products.Last(prod => prod.Color == search);
                }
                ResultText = $"Found : {value}";

            }
            catch 
            {
                ResultText = "Not Found";

            }
            Products.Clear();
        }
        public void LastOrDefault()
        {
            string search = "Red";
            Product value;
            if (UseQuerySyntax)
            {
                value = (from prod in Products select prod).LastOrDefault(prod => prod.Color == search);
            }
            else
            {
                value = Products.LastOrDefault(prod => prod.Color == search);
            }

            if (value == null)
            {
                ResultText = "Not Found";
            }
            else
            {
                ResultText = $"Found : {value}";
            }
            Products.Clear();
        }
        public void Single()
        {
            int search = 706;
            Product value;
            try
            {
                if (UseQuerySyntax)
                {
                    value = (from prod in Products select prod).Single(prod => prod.ProductID == search);
                }
                else
                {
                    value = Products.Single(prod => prod.ProductID == search);
                }
                ResultText = $"Found: {value}";

            }
            catch 
            {

                ResultText = "Not Found, or multiple elements found";

            }

            Products.Clear();
        }
        public void SingleOrDefault()
        {
            int search = 706;
            Product value;
            try
            {
                if (UseQuerySyntax)
                {
                    value = (from prod in Products select prod).SingleOrDefault(prod => prod.ProductID == search);
                }
                else
                {
                    value = Products.SingleOrDefault(prod => prod.ProductID == search);
                }

                if (value == null)
                {
                    ResultText = "Not Found";
                }
                else
                {
                    ResultText = $"Found : {value}";
                }
            }
            catch 
            {

                ResultText = "Multiple elements found";

            }
            Products.Clear();
        }
        public void ForEach()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products let tmp = prod.NameLength = prod.Name.Length select prod).ToList();
            }
            else
            {
                Products.ForEach(prod => prod.NameLength = prod.Name.Length);
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        private decimal SalesForProduct(Product prod)
        {
            return Sales.Where(sale => sale.ProductID == prod.ProductID)
                .Sum(sale => sale.LineTotal);
        }

        public void Take()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Name select prod).Take(5).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).Take(5).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
    }
}