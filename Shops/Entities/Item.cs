using Shops.Tools;
namespace Shops.Entities
{
    public class Item
    {
        private float _price;
        private int _number;
        public Item(Product systemProduct, float price, int number)
        {
            SystemProduct = systemProduct;
            Price = price;
            Number = number;
        }

        public Product SystemProduct { get; }

        public float Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ShopsException("Price of item cannot be < 0");
                _price = value;
            }
        }

        public int Number
        {
            get => _number;
            set
            {
                if (value < 0)
                    throw new ShopsException("Number of item cannot be < 0");
                _number = value;
            }
        }

        public override int GetHashCode()
        {
            return SystemProduct.GetHashCode();
        }
    }
}