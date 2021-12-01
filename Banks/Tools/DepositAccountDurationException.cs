namespace Banks.Tools
{
    public class DepositAccountDurationException : BanksException
    {
        public DepositAccountDurationException(string message)
            : base(message)
        {
        }
    }
}