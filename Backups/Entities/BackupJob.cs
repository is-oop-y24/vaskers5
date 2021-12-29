using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities.Algorithms;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackupJob
    {
        private int _lastRestorePointId = 0;
        public BackupJob(string jobPath, IAlgorithm algorithm, ISaver.ISaver saver)
        {
            JobPath = jobPath;
            Algorithm = algorithm;
            SubDirectoryPath = Path.Combine(jobPath, "SubDirectory");
            BackupFiles = new List<FileInfo>();
            Saver = saver;
            RestorePoints = new List<RestorePoint>() { };
        }

        public string JobPath { get; set; }
        public IAlgorithm Algorithm { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }

        public ISaver.ISaver Saver { get; }
        private string SubDirectoryPath { get; }
        private List<FileInfo> BackupFiles { get; set; }

        public virtual RestorePoint CreateNewRestorePoint()
        {
            var restorePointId = _lastRestorePointId++;
            var point = new RestorePoint(restorePointId, Path.Combine(SubDirectoryPath, $"{restorePointId}"));
            Algorithm.MakeBackup(point, BackupFiles, Saver);
            RestorePoints.Add(point);
            return point;
        }

        public void RemoveRestorePoint(int id)
        {
            RestorePoint point = RestorePoints.FirstOrDefault(point => point.RestorePointId == id);
            if (point is null)
                throw new RestorePointNotExistException("This restore point is not exist!");
            Directory.Delete(point.RestorePointPath);
        }

        public string FindFilePathInJob(FileInfo file)
        {
            return Directory.GetFiles(SubDirectoryPath).FirstOrDefault(filePath => filePath == file.Name);
        }

        public FileInfo RemoveFileFromJob(FileInfo file)
        {
            BackupFiles.Remove(file);

            var jobFile = new FileInfo(FindFilePathInJob(file));
            if (jobFile is null)
                throw new FileNotExistException("This file is not exist at current job");
            jobFile.Delete();

            return file;
        }

        public FileInfo AddFileToJob(FileInfo file)
        {
            BackupFiles.Add(file);
            return file;
        }

        public IReadOnlyCollection<FileInfo> GetBackupFiles()
        {
            return BackupFiles.AsReadOnly();
        }
    }
}