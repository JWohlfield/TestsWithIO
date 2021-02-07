using System;
using System.Collections.Generic;
using System.Text;

namespace FileIOSample
{
    class CSVProduct
    {
        public string ProductName;
        public ProductCategory FoodType;
        public string Description;
        public double Price;
        public int Quantity;

        public static CSVProduct FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            CSVProduct csvProducts = new CSVProduct();
            csvProducts.ProductName = Convert.ToString(values[0]);
            csvProducts.FoodType = (ProductCategory)Enum.Parse(typeof(ProductCategory), Convert.ToString(values[1]));
            csvProducts.Description = Convert.ToString(values[2]);
            csvProducts.Price = Convert.ToDouble(values[3]);
            csvProducts.Quantity = Convert.ToInt32(values[4]);
            return csvProducts;
        }
    }
}
