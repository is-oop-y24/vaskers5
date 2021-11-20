namespace Backups.Tools
{
    public class RestorePointNotExistException : BackupsException
    {
        public RestorePointNotExistException(string message)
            : base(message)
        {
        }
    }
}