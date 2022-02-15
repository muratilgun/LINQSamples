using System;
using LINQSamples.ViewModelClasses;

namespace LINQSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate the ViewModel
            SamplesViewModel vm = new SamplesViewModel { UseQuerySyntax = true};
            //Call a sample method
            vm.GetAllLooping();
            
            // Display Product Collection

            foreach (var item in vm.Products)
            {
                Console.WriteLine(item.ToString());   
            }

            // Display Result Text
            Console.WriteLine(vm.ResultText);
        }
    }
}
