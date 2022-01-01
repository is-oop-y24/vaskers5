using System;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Entities
{
    public class BackupsExtraConsoleLogger : IBackupsExtraLogger
    {
        public Logger CreateLogger()
        {
            string logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] [{SourceContext}] {Message}{NewLine}{Exception}";
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: logTemplate)
                .CreateLogger();
        }
    }
}