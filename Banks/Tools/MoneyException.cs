namespace Banks.Tools
{
    public class MoneyException : BanksException
    {
        public MoneyException(string message)
            : base(message)
        {
        }
    }
}