namespace Shops.Tools
{
    public class ProductAlreadyExistException : ShopsException
    {
        public ProductAlreadyExistException()
        {
        }

        public ProductAlreadyExistException(string message)
            : base(message)
        {
        }
    }
}