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
        }

        public bool UseQuerySyntax = true;
        public List<Product> Products { get; set; }
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
    }
}