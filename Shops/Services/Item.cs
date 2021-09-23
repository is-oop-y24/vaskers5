namespace Shops.Services
{
    public class Item
    {
        public Item(string name, float price, int number)
        {
            Name = name;
            Price = price;
            Number = number;
        }

        public string Name { get; }
        public float Price { get; set; }
        public int Number { get; set; }
    }
}