namespace Backups.Tools
{
    public class FileNotExistException : BackupsException
    {
        public FileNotExistException(string message)
            : base(message)
        {
        }
    }
}