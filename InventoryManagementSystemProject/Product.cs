namespace InventoryManagementSystemProject
{
    internal class Product
    {
        private static int productIdCounter = 0;
        public Product(string name, decimal price, int quantity, TType type)
        {
            ++productIdCounter; // Use provided ID or auto-generate
            Name = name;
            Price = price;
            Quantity = quantity;
            tType = type;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public TType tType { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Quantity: {Quantity}, Type: {tType}";
        }

        public static int GetProductCounter()
        {
            return productIdCounter;
        }
    }
}
