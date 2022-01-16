using System;
using System.IO;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Entities
{
    public class BackupsExtraFileLogger : IBackupsExtraLogger
    {
        public Logger CreateLogger()
        {
            string logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] [{SourceContext}] {Message}{NewLine}{Exception}";
            var loggerPath = Path.Combine(Environment.CurrentDirectory, @"\SavesDirectory\logs\log.txt");
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(loggerPath, outputTemplate: logTemplate)
                .CreateLogger();
        }
    }
}