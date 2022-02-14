using System.Collections.Generic;
using System.Linq;
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

        }

    }
}