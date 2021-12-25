namespace Banks.Tools
{
    public class FindAccountException : BanksException
    {
        public FindAccountException(string message)
            : base(message)
        {
        }
    }
}