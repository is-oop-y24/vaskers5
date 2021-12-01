namespace Banks.Tools
{
    public class LimitException : BanksException
    {
        public LimitException(string message)
            : base(message)
        {
        }
    }
}