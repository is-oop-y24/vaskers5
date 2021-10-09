namespace Shops.Tools
{
    public class ShopDontContainsItemException : ShopsException
    {
        public ShopDontContainsItemException()
        {
        }

        public ShopDontContainsItemException(string message)
            : base(message)
        {
        }
    }
}