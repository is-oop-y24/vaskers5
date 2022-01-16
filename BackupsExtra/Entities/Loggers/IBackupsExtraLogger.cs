using Serilog.Core;

namespace BackupsExtra.Entities
{
    public interface IBackupsExtraLogger
    {
         Logger CreateLogger();
    }
}