using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileIOSample
{
    class Program
    {
        public static List<Product> products = new List<Product>();
        static void Main(string[] args)
        {
            //The FileIO class will create a folder in your local APPDATA path
            //  and create files for the IO process if they don't already exist.

            //Then I have created some sample data below to use for testing how the IO process works
            
           
            
            //Adding items to the Product List
            products.Add(new Product("Italian Sub", ProductCategory.Sandwich, "Meatballs on a hoagie", 7.00, 0));
            products.Add(new Product("Chocolate Chip Cookie", ProductCategory.Dessert, "Made with love from your granny", 5.00, 0));

            Console.WriteLine($"products count = {products.Count()}");

            foreach (Product item in products)
            {
                Console.WriteLine($"Name: {item.ProductName}\tDescription: {item.Description}\tPrice: {item.Price}");
            }

            Console.WriteLine("\nThat was the products list.\nPress any key to continue...");
            Console.ReadLine();

            string testPath = FileIO.GetFilePath();
            string fullSalesPath = testPath + FileIO.GetSalesFileName(testPath);
            string fullCSVPath = testPath + FileIO.GetProductCSVName(testPath);

            FileIO.SaveToCsv(products, fullCSVPath);
            Console.WriteLine("CSV File created.\nPress any key to continue...");
            Console.ReadLine();

            //Convert a .csv file to a List
            List<CSVProduct> csvProducts = File.ReadAllLines(fullCSVPath)
                                                .Skip(1)
                                                .Select(v => CSVProduct.FromCsv(v))
                                                .ToList();
            Console.WriteLine($"csvProducts count = {csvProducts.Count()}\nPress any key to continue...\n");
            Console.ReadLine();

            foreach (CSVProduct products in csvProducts)
            {
                Console.WriteLine($"Name: {products.ProductName}\tDescription: {products.Description}\tPrice: {products.Price}");
            }

            Console.WriteLine("\nThat was the csvProducts list.\nPress any key to continue...");
            Console.ReadLine();

            FileIO.WriteToFile(fullSalesPath, "02/05/2021 $25.68");
            FileIO.WriteToFile(fullSalesPath, "02/05/2021 $13.25");
            FileIO.WriteToFile(fullSalesPath, "02/06/2021 $67.12");

            //Console.ReadLine();
        }
    }
}
