namespace IsuExtra.Tools
{
    public class InvalidDayException : IsuExtraException
    {
        public InvalidDayException(string message)
            : base(message)
        {
        }
    }
}