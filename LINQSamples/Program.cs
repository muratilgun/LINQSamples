using System;
using LINQSamples.ViewModelClasses;

namespace LINQSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate the ViewModel
            SamplesViewModel vm = new SamplesViewModel { UseQuerySyntax = false};
            //Call a sample method
            vm.AggregateCustom();

            // Display Product Collection
            //foreach (var item in vm.Products)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            // Display Result Text
            Console.WriteLine(vm.ResultText);
        }
    }
}
