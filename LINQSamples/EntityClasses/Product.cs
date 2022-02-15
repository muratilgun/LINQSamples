﻿using System.Text;

namespace LINQSamples.EntityClasses
{
    public partial class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        #region ToString Override 
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(1024);

            sb.Append(Name);
            sb.AppendLine($"  ID: {ProductID}");
            sb.Append($"   Color: {Color}");
            sb.AppendLine($"   Size: {(Size ?? "n/a")}");
            sb.Append($"   Cost: {StandardCost:c}");
            sb.AppendLine($"   Price: {ListPrice:c}");

            return sb.ToString();
        }
        #endregion
    }
}