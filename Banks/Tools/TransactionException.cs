namespace Banks.Tools
{
    public class TransactionException : BanksException
    {
        public TransactionException(string message)
            : base(message)
        {
        }
    }
}