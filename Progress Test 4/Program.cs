using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagementSystem
{
    class Program
    {
        static Shop shop = new Shop();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("PRODUCT MANAGEMENT SYSTEM");
                Console.WriteLine("1. Add new product");
                Console.WriteLine("2. Remove product");
                Console.WriteLine("3. Iterate product list");
                Console.WriteLine("4. Search product");
                Console.WriteLine("5. Sort product by price");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        shop.AddProduct();
                        break;
                    case "2":
                        shop.RemoveProduct();
                        break;
                    case "3":
                        shop.IterateProductList();
                        break;
                    case "4":
                        shop.SearchProduct();
                        break;
                    case "5":
                        shop.SortProduct();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

    class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<int> Rate { get; set; }

        public void ViewInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Average rating: {CalculateAverageRating()}");
        }

        private double CalculateAverageRating()
        {
            if (Rate == null || Rate.Count == 0)
                return 0;

            return Rate.Average();
        }
    }

    class Shop
    {
        public List<Product> ProductList { get; set; }

        public Shop()
        {
            ProductList = new List<Product>();
        }

        public void AddProduct()
        {
            Product product = new Product();

            Console.Write("Enter product name: ");
            product.Name = Console.ReadLine();

            Console.Write("Enter product description: ");
            product.Description = Console.ReadLine();

            Console.Write("Enter product price (0 < price < 100): ");
            product.Price = double.Parse(Console.ReadLine());

            Console.Write("Enter product ratings (comma-separated, e.g., 1,2,3): ");
            string[] ratings = Console.ReadLine().Split(',');
            product.Rate = new List<int>();

            foreach (string rating in ratings)
            {
                product.Rate.Add(int.Parse(rating));
            }

            ProductList.Add(product);
            Console.WriteLine("Product added successfully!");
        }

        public void RemoveProduct()
        {
            Console.Write("Enter product name to remove: ");
            string productName = Console.ReadLine();

            Product productToRemove = ProductList.FirstOrDefault(p => p.Name == productName);

            if (productToRemove != null)
            {
                ProductList.Remove(productToRemove);
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void IterateProductList()
        {
            foreach (Product product in ProductList)
            {
                product.ViewInfo();
                Console.WriteLine();
            }
        }

        public void SearchProduct()
        {
            Console.Write("Enter lower price limit: ");
            double lowerPrice = double.Parse(Console.ReadLine());

            Console.Write("Enter upper price limit: ");
            double upperPrice = double.Parse(Console.ReadLine());

            List<Product> filteredProducts = ProductList.Where(p => p.Price >= lowerPrice && p.Price <= upperPrice).ToList();

            if (filteredProducts.Count == 0)
            {
                Console.WriteLine("No products found within the specified price range.");
            }
            else
            {
                Console.WriteLine("Products within the specified price range:");
                foreach (Product product in filteredProducts)
                {
                    product.ViewInfo();
                    Console.WriteLine();
                }
            }
        }

        public void SortProduct()
        {
            ProductList = ProductList.OrderBy(p => p.Price).ToList();
            Console.WriteLine("Product list sorted by price.");
        }
    }
}
