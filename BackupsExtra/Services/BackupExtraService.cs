using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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
    public class BackupExtraService
    {
        public BackupExtraService()
        {
            BackupJobs = new List<BackupExtraJob>() { };
            ConsoleLogger = new BackupsExtraConsoleLogger().CreateLogger();
            FileLogger = new BackupsExtraFileLogger().CreateLogger();
        }

        public Logger ConsoleLogger { get; }

        public Logger FileLogger { get; }

        public List<BackupExtraJob> BackupJobs { get; set; }

        public BackupExtraJob AddNewJob(
            string jobPath,
            IAlgorithm algorithm,
            ISaver saver,
            IClearAlgorithm clearAlgorithm,
            IRestorePointsJob restorePointsJob)
        {
            var job = new BackupExtraJob(
                jobPath,
                algorithm,
                saver,
                clearAlgorithm,
                restorePointsJob,
                ConsoleLogger,
                FileLogger);
            string msg = $"Create new job with path: {jobPath}";
            ConsoleLogger.Information(msg);
            FileLogger.Information(msg);
            BackupJobs.Add(job);
            return job;
        }

        public BackupExtraJob RemoveJob(BackupExtraJob job)
        {
            BackupJobs.Remove(job);
            string msg = $"Successfully delete job with path: {job.JobPath}";
            ConsoleLogger.Information(msg);
            FileLogger.Information(msg);
            return job;
        }

        public FileInfo AddFileToJob(BackupExtraJob job, string filePath)
        {
            var file = new FileInfo(filePath);
            job.AddFileToJob(file);
            string msg = $"Successfully add {filePath} to job with path {job.JobPath}";
            ConsoleLogger.Information(msg);
            FileLogger.Information(msg);
            return file;
        }

        public void Save()
        {
            var save = JsonSerializer.Serialize(BackupJobs);
            var path = Path.Combine(Environment.CurrentDirectory, @"\SavesDirectory");
            File.WriteAllText("save.json", save);
            string msg = $"Successfully saved all program work to system backup!";
            ConsoleLogger.Information(msg);
            FileLogger.Information(msg);
        }

        public BackupExtraService Load()
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"\SavesDirectory\save.json");
            var save = File.ReadAllText(path);
            BackupJobs = JsonSerializer.Deserialize<List<BackupExtraJob>>(save);
            string msg = $"Successfully loads from backup!";
            ConsoleLogger.Information(msg);
            FileLogger.Information(msg);
            return this;
        }
    }
}