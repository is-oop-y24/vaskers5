namespace Shops.Tools
{
    public class ItemDontExistException : ShopsException
    {
        public ItemDontExistException()
        {
        }

        public ItemDontExistException(string message)
            : base(message)
        {
        }
    }
}