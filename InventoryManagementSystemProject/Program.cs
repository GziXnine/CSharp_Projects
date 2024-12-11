using System.Net.Sockets;

namespace InventoryManagementSystemProject
{
    public enum TType
    {
        Available,
        Discontinued,
        Unavailable
    }

    internal class Program
    {
        static Dictionary<int, Product> products = new Dictionary<int, Product>();
        static int counterID = 0; 

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Inventory Management System!\n");
            Console.ResetColor();

            Product[] tickets = new Product[]
            {
                new Product("T-Shirt", 150, 100, TType.Available),
                new Product("Hat", 110, 20, TType.Available),
                new Product("Suits", 70, 40, TType.Discontinued),
                new Product("Jacket", 13, 100, TType.Available),
                new Product("Nickels", 201, 70, TType.Discontinued),
                new Product("Bag", 1000, 2, TType.Unavailable)
            };

            foreach (Product ticket in tickets)
            {
                products[++counterID] = ticket;
            }

            while (true)
            {
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. Remove a product");
                Console.WriteLine("3. Update a product");
                Console.WriteLine("4. Display all products");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice: ");

                Console.ForegroundColor = ConsoleColor.Blue;
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        RemoveProduct();
                        break;
                    case 3:
                        UpdateProduct();
                        break;
                    case 4:
                        DisplayProduct();
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nThank you for using the Inventory Management System!");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void AddProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter product price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter product quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter product type (0: Available, 1: Discontinued): ");
            TType type = (TType)Convert.ToInt32(Console.ReadLine());
            
            var product = new Product(name, price, quantity, type);

            Console.Write("Enter custom product ID (or leave blank for auto-generated): ");
            var input = Console.ReadLine();
            int customId = string.IsNullOrWhiteSpace(input) ? Product.GetProductCounter() : int.Parse(input);

            products.Add(customId, product);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Product added successfully with ID: {customId}");
            Console.WriteLine(product.ToString());
            Console.ResetColor();
        }

        private static void DisplayProduct()
        {
            Console.WriteLine("\nAll products:");
            foreach (var product in products)
            {
                if(product.Value.tType == TType.Available)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (product.Value.tType == TType.Discontinued)
                    Console.ForegroundColor = ConsoleColor.Cyan; 
                else
                    Console.ForegroundColor = ConsoleColor.Red; 

                Console.Write($"Id: {product.Key}, ");
                Console.WriteLine(product.Value);
                Console.ResetColor();
            }
        }

        private static void UpdateProduct()
        {
            Console.Write("Enter product ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (products.ContainsKey(id))
            {
                Console.WriteLine("\nCurrent product details:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(products[id]);
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("Which details would you like to update? (Press Enter to skip)");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Price");
                Console.WriteLine("3. Quantity");
                Console.WriteLine("4. Type");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        Console.Write("Enter new product name: ");
                        string name = Console.ReadLine() ?? string.Empty;
                        products[id].Name = name;
                        break;
                    case 2:
                        Console.Write("Enter new product price: ");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        products[id].Price = price;
                        break;
                    case 3:
                        Console.Write("Enter new product quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        products[id].Quantity = quantity;
                        break;
                    case 4:
                        Console.Write("Enter new product type (0: Available, 1: Discontinued, 2: Unavailable): ");
                        TType type = (TType)Convert.ToInt32(Console.ReadLine());
                        products[id].tType = type;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Product with ID {id} updated successfully");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found!");
                Console.ResetColor();
            }
        }

        private static void RemoveProduct()
        {
            Console.Write("Please Enter A Product ID To Remove: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            int id = Convert.ToInt32(Console.ReadLine());

            if (!products.ContainsKey(id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This Product Doesn't Exits");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                products.Remove(id);
                Console.WriteLine($"Item With ID: {id} Is Removed Successfully.");
            }
            Console.ResetColor();
        }
    }
}
