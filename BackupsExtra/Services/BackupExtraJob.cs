using Backups.Entities;
using Backups.Entities.Algorithms;
using Backups.Entities.ISaver;
using BackupsExtra.Entities;
using BackupsExtra.Entities.RestorePointsCleaningAlgorithms;
using BackupsExtra.Entities.RestorePointsJob;
using BackupsExtra.Tools;
using Serilog.Core;

namespace BackupsExtra.Services
{
    public class BackupExtraJob : BackupJob
    {
        public BackupExtraJob(
            string jobPath,
            IAlgorithm algorithm,
            ISaver saver,
            IClearAlgorithm clearAlgorithm,
            IRestorePointsJob restorePointJob,
            Logger consoleLogger,
            Logger fileLogger)
            : base(jobPath, algorithm, saver)
        {
            ClearAlgorithm = clearAlgorithm;
            RestorePointJob = restorePointJob;
            ConsoleLogger = consoleLogger;
            FileLogger = fileLogger;
        }

        public IClearAlgorithm ClearAlgorithm { get; }
        public IRestorePointsJob RestorePointJob { get; }
        public Logger ConsoleLogger { get; }
        public Logger FileLogger { get; }

        public override RestorePoint CreateNewRestorePoint()
        {
            var point = base.CreateNewRestorePoint();
            var invalidPoints = ClearAlgorithm.MakeClear(RestorePoints);
            if (invalidPoints is null)
                return point;
            if (invalidPoints.Count == RestorePoints.Count)
            {
                throw new ClearLimitException("Please check your's restore points limit!");
            }

            string msg = $"Successfully create new Restore point!";
            ConsoleLogger.Information(msg);
            FileLogger.Information(msg);
            RestorePoints = RestorePointJob.DoJob(RestorePoints, invalidPoints);
            return point;
        }
    }
}