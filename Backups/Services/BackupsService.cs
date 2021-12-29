using System;
using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Entities.Algorithms;
using Backups.Entities.ISaver;

namespace Backups.Services
{
    public class BackupsService
    {
        public BackupsService()
        {
            BackupJobs = new List<BackupJob>() { };
        }

        public List<BackupJob> BackupJobs { get; set; }

        public BackupJob AddNewJob(string jobPath, IAlgorithm algorithm, ISaver saver)
        {
            var job = new BackupJob(jobPath, algorithm, saver);
            BackupJobs.Add(job);
            return job;
        }

        public BackupJob RemoveJob(BackupJob job)
        {
            BackupJobs.Remove(job);
            Directory.Delete(job.JobPath);
            return job;
        }

        public FileInfo AddFileToJob(BackupJob job, string filePath)
        {
            var file = new FileInfo(filePath);
            job.AddFileToJob(file);
            return file;
        }
    }
}