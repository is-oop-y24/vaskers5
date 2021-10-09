namespace Shops.Tools
{
    public class NotEnoughMoneyException : ShopsException
    {
        public NotEnoughMoneyException()
        {
        }

        public NotEnoughMoneyException(string message)
            : base(message)
        {
        }
    }
}