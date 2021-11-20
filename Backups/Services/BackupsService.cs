using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Services
{
    public class BackupsService
    {
        public BackupsService()
        {
            BackupJobs = new List<BackupJob>() { };
        }

        public List<BackupJob> BackupJobs { get; set; }

        public BackupJob AddNewJob(string jobPath, BackupAlgorithms algorithm, bool isVirtual)
        {
            var job = new BackupJob(jobPath, algorithm, isVirtual);
            BackupJobs.Add(job);
            return job;
        }

        public BackupJob RemoveJob(BackupJob job)
        {
            BackupJobs.Remove(job);
            Directory.Delete(job.JobPath);
            return job;
        }

        public SystemFile AddFileToJob(BackupJob job, string filePath)
        {
            var file = new SystemFile(filePath, DateTime.Now);
            job.AddFileToJob(file);
            return file;
        }
    }
}