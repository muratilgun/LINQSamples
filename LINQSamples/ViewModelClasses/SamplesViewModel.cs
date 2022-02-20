using System;
using System.Collections.Generic;
using System.Globalization;
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
                list.AddRange(from prod in Products select prod.Name);
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
                    ProductSize = prod.Size
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
        public void TakeWhile()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Name select prod).TakeWhile(prod => prod.Name.StartsWith("A")).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).TakeWhile(prod => prod.Name.StartsWith("A")).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";

        }
        public void Skip()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Name select prod).Skip(20).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).Skip(20).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";
        }
        public void SkipWhile()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products orderby prod.Name select prod).SkipWhile(prod => prod.Name.StartsWith("A")).ToList();
            }
            else
            {
                Products = Products.OrderBy(prod => prod.Name).SkipWhile(prod => prod.Name.StartsWith("A")).ToList();
            }

            ResultText = $"Total Products: {Products.Count}";

        }
        public void Distinct()
        {
            List<string> colors;
            if (UseQuerySyntax)
            {
                colors = (from prod in Products select prod.Color).Distinct().ToList();
            }
            else
            {
                colors = Products.Select(prod => prod.Color).Distinct().ToList();
            }

            foreach (var color in colors)
            {
                Console.WriteLine($"Color: {color}");
            }

            Console.WriteLine($"Total Colors: {colors.Count}");
            Products.Clear();

        }
        public void All()
        {
            string search = " ";
            bool value;
            if (UseQuerySyntax)
            {
                value = (from prod in Products select prod).All(prod => prod.Name.Contains(search));
            }
            else
            {
                value = Products.All(prod => prod.Name.Contains(search));
            }

            ResultText = $"Do all Name properties contain a '{search}'? {value}";
            Products.Clear();
        }
        public void Any()
        {
            string search = "z";
            bool value;
            if (UseQuerySyntax)
            {
                value = (from prod in Products select prod).Any(prod => prod.Name.Contains(search));
            }
            else
            {
                value = Products.Any(prod => prod.Name.Contains(search));
            }

            ResultText = $"Do any Name properties contain a '{search}'? {value}";
            Products.Clear();
        }
        public void LINQContains()
        {
            bool value;
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            if (UseQuerySyntax)
            {
                value = (from num in numbers select num).Contains(3);
            }
            else
            {
                value = numbers.Contains(3);
            }

            ResultText = $"Is the number in collection? {value}";
            Products.Clear();
        }
        public void LINQContainsUsingComparer()
        {
            int search = 744;
            bool value;
            ProductIdComparer pc = new ProductIdComparer();
            Product prodToFind = new Product { ProductID = search };
            if (UseQuerySyntax)
            {
                value = (from prod in Products select prod).Contains(prodToFind, pc);
            }
            else
            {
                value = Products.Contains(prodToFind, pc);
            }

            ResultText = $"Product ID: {search} is in Products Collection = {value}";
        }
        public void SequenceEqualIntegers()
        {
            bool value;
            List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };
            if (UseQuerySyntax)
            {
                value = (from num in list1 select num).SequenceEqual(list2);
            }
            else
            {
                value = list1.SequenceEqual(list2);
            }
            if (value)
            {
                ResultText = "Lists are Equal";
            }
            else
            {
                ResultText = "Lists are NOT Equal";
            }
        }
        public void SequenceEqualProducts()
        {
            bool value;
            List<Product> list1 = new List<Product>
            {
                new Product{ProductID = 1,Name = "Product 1"},
                new Product{ProductID = 2,Name = "Product 2"}
            };

            List<Product> list2 = new List<Product>
            {
                new Product{ProductID = 1,Name = "Product 1"},
                new Product{ProductID = 2,Name = "Product 2"}
            };
            if (UseQuerySyntax)
            {
                value = (from prod in list1 select prod).SequenceEqual(list2);
            }
            else
            {
                value = list1.SequenceEqual(list2);
            }

            if (value)
            {
                ResultText = "Lists are Equal";
            }
            else
            {
                ResultText = "Lists are NOT Equal";
            }
        }
        public void SequenceEqualUsingComparer()
        {
            bool value;
            ProductComparer pc = new ProductComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();
            list1.RemoveAt(0);
            if (UseQuerySyntax)
            {
                value = (from prod in list1 select prod).SequenceEqual(list2, pc);
            }
            else
            {
                value = list1.SequenceEqual(list2, pc);
            }
            if (value)
            {
                ResultText = "Lists are Equal";
            }
            else
            {
                ResultText = "Lists are NOT Equal";
            }
        }
        public void ExceptIntegers()
        {
            List<int> exceptions;
            List<int> list1 = new List<int> { 1, 2, 3, 4 };
            List<int> list2 = new List<int> { 3, 4, 5 };
            if (UseQuerySyntax)
            {
                exceptions = (from num in list1 select num).Except(list2).ToList();
            }
            else
            {
                exceptions = list1.Except(list2).ToList();
            }

            ResultText = string.Empty;
            foreach (var item in exceptions)
            {
                ResultText += "Number: " + item + Environment.NewLine;
            }
            Products.Clear();
        }
        public void ExceptProducts()
        {
            ProductComparer pc = new ProductComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();
            list2.RemoveAll(prod => prod.Color == "Black");
            if (UseQuerySyntax)
            {
                Products = (from prod in list1 select prod).Except(list2, pc).ToList();
            }
            else
            {
                Products = list1.Except(list2, pc).ToList();
            }

            ResultText = $"Total Products : {Products.Count}";
        }
        public void Intersect()
        {
            //Her iki listedeki ortak objeleri bulur.
            ProductComparer pc = new ProductComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();
            list1.RemoveAll(prod => prod.Color == "Black");
            list2.RemoveAll(prod => prod.Color == "Red");
            if (UseQuerySyntax)
            {
                Products = (from prod in list1 select prod).Intersect(list2, pc).ToList();
            }
            else
            {
                Products = list1.Intersect(list2, pc).ToList();
            }

            ResultText = $"Total Products : {Products.Count}";
        }
        public void Union()
        {
            //İki listede sadece farklı nesneleri bir araya getirirler t-sql union
            ProductComparer pc = new ProductComparer();
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();
            if (UseQuerySyntax)
            {
                Products = (from prod in list1 select prod).Union(list2, pc).OrderBy(prod => prod.Name).ToList();
            }
            else
            {
                Products = list1.Union(list2, pc).OrderBy(prod => prod.Name).ToList();
            }
            ResultText = $"Total Products : {Products.Count}";
        }
        public void LINQConcat()
        {
            //İki listenin tamamını bir araya getirirler t-sql unionall
            List<Product> list1 = ProductRepository.GetAll();
            List<Product> list2 = ProductRepository.GetAll();
            if (UseQuerySyntax)
            {
                Products = (from prod in list1 select prod).Concat(list2).OrderBy(prod => prod.Name).ToList();
            }
            else
            {
                Products = list1.Concat(list2).OrderBy(prod => prod.Name).ToList();
            }
            ResultText = $"Total Products : {Products.Count}";
        }
        public void InnerJoin()
        {
            StringBuilder sb = new StringBuilder(2048);
            int count = 0;
            if (UseQuerySyntax)
            {
                var query = (from prod in Products
                             join sale in Sales on prod.ProductID equals sale.ProductID
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Color,
                                 prod.StandardCost,
                                 prod.ListPrice,
                                 prod.Size,
                                 sale.SalesOrderID,
                                 sale.OrderQty,
                                 sale.UnitPrice,
                                 sale.LineTotal
                             });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"  Product ID: {item.ProductID}");
                    sb.AppendLine($"  Product Name: {item.Name}");
                    sb.AppendLine($"  Size: {item.Size}");
                    sb.AppendLine($"  Order Qty: {item.OrderQty}");
                    sb.AppendLine($"  Total: {item.LineTotal:c}");
                }
            }
            else
            {
                var query = Products.Join(Sales, prod => prod.ProductID, sale => sale.ProductID, (prod, sale) => new
                {
                    prod.ProductID,
                    prod.Name,
                    prod.Color,
                    prod.StandardCost,
                    prod.ListPrice,
                    prod.Size,
                    sale.SalesOrderID,
                    sale.OrderQty,
                    sale.UnitPrice,
                    sale.LineTotal
                });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"  Product ID: {item.ProductID}");
                    sb.AppendLine($"  Product Name: {item.Name}");
                    sb.AppendLine($"  Size: {item.Size}");
                    sb.AppendLine($"  Order Qty: {item.OrderQty}");
                    sb.AppendLine($"  Total: {item.LineTotal.ToString("C", new CultureInfo("en-US"))}");
                }
            }

            ResultText = sb.ToString() + Environment.NewLine + $"Total Sales : {count.ToString()}";
        }
        public void InnerJoinTwoFields()
        {
            short qty = 6;
            int count = 0;
            StringBuilder sb = new StringBuilder(2048);
            if (UseQuerySyntax)
            {
                var query = (from prod in Products
                             join sale in Sales on new { prod.ProductID, Qty = qty } equals new
                             { sale.ProductID, Qty = sale.OrderQty }
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Color,
                                 prod.StandardCost,
                                 prod.ListPrice,
                                 prod.Size,
                                 sale.SalesOrderID,
                                 sale.OrderQty,
                                 sale.UnitPrice,
                                 sale.LineTotal
                             });
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"  Product ID: {item.ProductID}");
                    sb.AppendLine($"  Product Name: {item.Name}");
                    sb.AppendLine($"  Size: {item.Size}");
                    sb.AppendLine($"  Order Qty: {item.OrderQty}");
                    sb.AppendLine($"  Total: {item.LineTotal.ToString("C", new CultureInfo("en-US"))}");
                }
            }
            else
            {
                var query = Products.Join(Sales, prod => new { prod.ProductID, Qty = qty },
                    sale => new { sale.ProductID, Qty = sale.OrderQty }, (prod, sale) => new
                    {
                        prod.ProductID,
                        prod.Name,
                        prod.Color,
                        prod.StandardCost,
                        prod.ListPrice,
                        prod.Size,
                        sale.SalesOrderID,
                        sale.OrderQty,
                        sale.UnitPrice,
                        sale.LineTotal
                    });

                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Sales Order: {item.SalesOrderID}");
                    sb.AppendLine($"  Product ID: {item.ProductID}");
                    sb.AppendLine($"  Product Name: {item.Name}");
                    sb.AppendLine($"  Size: {item.Size}");
                    sb.AppendLine($"  Order Qty: {item.OrderQty}");
                    sb.AppendLine($"  Total: {item.LineTotal.ToString("C", new CultureInfo("en-US"))}");
                }
            }

            ResultText = sb.ToString() + Environment.NewLine + $"Total Sales : {count.ToString()}";

        }
        public void GroupJoin()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<ProductSales> grouped;
            if (UseQuerySyntax)
            {
                grouped = (from prod in Products
                           join sale in Sales on prod.ProductID equals sale.ProductID into sales
                           select new ProductSales
                           {
                               Product = prod,
                               Sales = sales
                           });
            }
            else
            {
                grouped = Products.GroupJoin(Sales,
                    prod => prod.ProductID,
                    sale => sale.ProductID,
                    (prod, sales) => new ProductSales
                    {
                        Product = prod,
                        Sales = sales.ToList()
                    });
            }

            foreach (var ps in grouped)
            {
                sb.Append($"Product: {ps.Product}");
                if (ps.Sales.Count() > 0)
                {
                    sb.AppendLine("   ** Sales **");
                    foreach (var sale in ps.Sales)
                    {
                        sb.Append($"   SalesOrdereID: {sale.SalesOrderID}");
                        sb.Append($"   Qty: {sale.OrderQty}");
                        sb.AppendLine($"     Total: {sale.LineTotal}");
                    }
                }
                else
                {
                    sb.AppendLine("   ** NO Sales for Product **");
                }

                sb.AppendLine("");
            }

            ResultText = sb.ToString();
        }
        public void LeftOuterJoin()
        {
            //tüm ürünleri getir eğer satış varsa satışları yazdır. yoksa boş değer döndür.
            //Sağ taraf null değeri ifade ediyor.
            StringBuilder sb = new StringBuilder(2048);
            int count = 0;
            if (UseQuerySyntax)
            {
                var query = (from prod in Products
                             join sale in Sales
                                 on prod.ProductID equals sale.ProductID into sales
                             from sale in sales.DefaultIfEmpty()
                             select new
                             {
                                 prod.ProductID,
                                 prod.Name,
                                 prod.Color,
                                 prod.StandardCost,
                                 prod.ListPrice,
                                 prod.Size,
                                 sale?.SalesOrderID,
                                 sale?.OrderQty,
                                 sale?.UnitPrice,
                                 sale?.LineTotal
                             }).OrderBy(ps => ps.Name);
                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Product Name: {item.Name} ({item.ProductID})");
                    sb.AppendLine($"   Order ID: {item.SalesOrderID}");
                    sb.AppendLine($"   Size: {item.Size}");
                    sb.AppendLine($"   Order Qty: {item.OrderQty}");
                    sb.AppendLine($"   Total: {item.LineTotal?.ToString("C", new CultureInfo("en-US"))}");
                } 
            }
            else
            {
                var query = Products.SelectMany(
                    sale =>
                        Sales.Where(s => sale.ProductID == s.ProductID).DefaultIfEmpty(),
                    (prod, sale) => new
                    {
                        prod.ProductID,
                        prod.Name,
                        prod.Color,
                        prod.StandardCost,
                        prod.ListPrice,
                        prod.Size,
                        sale?.SalesOrderID,
                        sale?.OrderQty,
                        sale?.UnitPrice,
                        sale?.LineTotal
                    }).OrderBy(ps => ps.Name);

                foreach (var item in query)
                {
                    count++;
                    sb.AppendLine($"Product Name: {item.Name} ({item.ProductID})");
                    sb.AppendLine($"  Order ID: {item.SalesOrderID}");
                    sb.AppendLine($"  Size: {item.Size}");
                    sb.AppendLine($"  Order Qty: {item.OrderQty}");
                    sb.AppendLine($"  Total: {item.LineTotal?.ToString("C", new CultureInfo("en-US"))}");
                }
            }

            ResultText = sb.ToString() + Environment.NewLine + "Total Sales: " + count.ToString();
        }
        public void GroupBy()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            if (UseQuerySyntax)
            {
                sizeGroup = (from prod in Products orderby prod.Size group prod by prod.Size);
            }
            else
            {
                sizeGroup=Products.OrderBy(prod=>prod.Size)
                                  .GroupBy(prod=>prod.Size);
            }

            foreach (var group in sizeGroup)
            {
                sb.AppendLine($"Size: {group.Key} Count: {group.Count()}");
                foreach (var prod in group)
                {
                    sb.Append($"  ProductID:{prod.ProductID}");
                    sb.Append($"  Name: {prod.Name}");
                    sb.AppendLine($"  Color: {prod.Color}");
                }
            }

            ResultText = sb.ToString();
        }
        public void GroupByIntoSelect()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            if (UseQuerySyntax)
            {
                sizeGroup = (from prod in Products orderby prod.Size group prod by prod.Size into sizes select sizes);

            }
            else
            {
                sizeGroup = Products.OrderBy(prod => prod.Size).GroupBy(prod => prod.Size);

            }
            foreach (var group in sizeGroup)
            {
                sb.AppendLine($"Size: {group.Key} Count: {group.Count()}");
                foreach (var prod in group)
                {
                    sb.Append($"  ProductID:{prod.ProductID}");
                    sb.Append($"  Name: {prod.Name}");
                    sb.AppendLine($"  Color: {prod.Color}");
                }
            }

            ResultText = sb.ToString();
        }
        public void GroupByOrderByKey()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            if (UseQuerySyntax)
            {
                sizeGroup = (from prod in Products group  prod by prod.Size into sizes orderby  sizes.Key select sizes);

            }
            else
            {
                sizeGroup = Products.GroupBy(prod => prod.Size).OrderBy(sizes => sizes.Key).Select(sizes => sizes);

            }
            foreach (var group in sizeGroup)
            {
                sb.AppendLine($"Size: {group.Key} Count: {group.Count()}");
                foreach (var prod in group)
                {
                    sb.Append($"  ProductID:{prod.ProductID}");
                    sb.Append($"  Name: {prod.Name}");
                    sb.AppendLine($"  Color: {prod.Color}");
                }
            }

            ResultText = sb.ToString();
        }
        public void GroupByWhere()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<IGrouping<string, Product>> sizeGroup;
            if (UseQuerySyntax)
            {
                sizeGroup = (from prod in Products group prod by prod.Size into sizes where sizes.Count() > 2 select sizes);

            }
            else
            {
                sizeGroup = Products.GroupBy(prod => prod.Size)
                    .Where(sizes=> sizes.Count() > 2)
                    .Select(sizes => sizes);

            }
            foreach (var group in sizeGroup)
            {
                sb.AppendLine($"Size: {group.Key} Count: {group.Count()}");
                foreach (var prod in group)
                {
                    sb.Append($"  ProductID:{prod.ProductID}");
                    sb.Append($"  Name: {prod.Name}");
                    sb.AppendLine($"  Color: {prod.Color}");
                }
            }

            ResultText = sb.ToString();
        }

        public void GroupedSubquery()
        {
            StringBuilder sb = new StringBuilder(2048);
            IEnumerable<SaleProducts> salesGroup;

            if (UseQuerySyntax)
            {
                salesGroup = (from sale in Sales
                    group sale by sale.SalesOrderID into sales
                    select new SaleProducts
                    {
                        SalesOrderID = sales.Key,
                        Products = (from prod in Products
                            join sale in Sales on prod.ProductID equals sale.ProductID
                            where sale.SalesOrderID == sales.Key
                            select prod).ToList()
                    });
            }
            else
            {
                // Method syntax
                salesGroup =
                    Sales.GroupBy(sale => sale.SalesOrderID)
                        .Select(sales => new SaleProducts
                        {
                            SalesOrderID = sales.Key,
                            Products = Products.Join(sales,
                                prod => prod.ProductID,
                                sale => sale.ProductID,
                                (prod, sale) => prod).ToList()
                        });
            }

            foreach (var sale in salesGroup)
            {
                sb.AppendLine($"Sales ID: {sale.SalesOrderID}");

                if (sale.Products.Count > 0)
                {
                    foreach (var prod in sale.Products)
                    {
                        sb.Append($"  ProductID: {prod.ProductID}");
                        sb.Append($"  Name: {prod.Name}");
                        sb.AppendLine($"  Color: {prod.Color}");
                    }
                }
                else
                {
                    sb.AppendLine("   Product ID not found for this sale.");
                }
            }

            ResultText = sb.ToString();
        }
    }
}