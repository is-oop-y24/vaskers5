using Shops.Tools;
namespace Shops.Entities
{
    public class Item
    {
        public Item(string name, float price, int number)
        {
            if (price < 0)
                throw new ShopsException("Price of item cannot be < 0");
            if (number < 0)
                throw new ShopsException("Number of item cannot be < 0");
            Name = name;
            Price = price;
            Number = number;
        }

        public string Name { get; }
        public float Price { get; set; }
        public int Number { get; set; }
    }
}