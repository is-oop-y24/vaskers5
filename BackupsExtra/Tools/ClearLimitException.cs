namespace BackupsExtra.Tools
{
    public class ClearLimitException : BackupsExtraException
    {
        public ClearLimitException(string message)
            : base(message)
        {
        }
    }
}