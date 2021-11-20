using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackupJob
    {
        private int _lastId = 0;
        private string _subDirectoryPath = string.Empty;
        public BackupJob(string jobPath, BackupAlgorithms algorithm, List<SystemFile> files)
        {
            JobPath = jobPath;
            Algorithm = algorithm;
            _subDirectoryPath = jobPath + "/" + "SubDirectory";
            Directory.CreateDirectory(_subDirectoryPath);
            files.Select(file => file.JustFile.CopyTo(_subDirectoryPath));
            BackupFiles = files;
            RestorePoints = new List<RestorePoint>() { };
        }

        public BackupJob(string jobPath, BackupAlgorithms algorithm)
            : this(jobPath, algorithm, new List<SystemFile>() { })
        {
        }

        public string JobPath { get; set; }
        public BackupAlgorithms Algorithm { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }
        private List<SystemFile> BackupFiles { get; set; }

        public virtual RestorePoint CreateNewRestorePoint()
        {
            string restorePointPath = JobPath + "_" + _lastId++;
            Directory.CreateDirectory(restorePointPath);
            RestorePoint restorePoint;
            switch (Algorithm)
            {
                case BackupAlgorithms.Split:
                    var archives = BackupFiles.Select(file => ZipFile.Open(restorePointPath, ZipArchiveMode.Create).CreateEntryFromFile(file.JustFile.FullName, file.JustFile.Name).Archive).ToList();
                    restorePoint = new RestorePoint(_lastId, restorePointPath,  archives);
                    break;
                case BackupAlgorithms.Single:
                    ZipFile.CreateFromDirectory(_subDirectoryPath, restorePointPath);
                    ZipArchive archive = ZipFile.Open(restorePointPath, ZipArchiveMode.Read);
                    restorePoint = new RestorePoint(_lastId, restorePointPath, archive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            RestorePoints.Add(restorePoint);
            return restorePoint;
        }

        public void RemoveRestorePoint(int id)
        {
            RestorePoint point = RestorePoints.FirstOrDefault(point => point.Id == id);
            if (point is null)
                throw new RestorePointNotExistException("This restore point is not exist!");
            Directory.Delete(point.RestorePointPath);
        }

        public string FindFilePathInJob(SystemFile file)
        {
            return Directory.GetFiles(_subDirectoryPath).FirstOrDefault(filePath => filePath == file.JustFile.Name);
        }

        public SystemFile RemoveFileFromJob(SystemFile file)
        {
            BackupFiles.Remove(file);
            var jobFile = new FileInfo(FindFilePathInJob(file));
            if (jobFile is null)
                throw new FileNotExistException("This file is not exist at current job");
            jobFile.Delete();
            return file;
        }

        public SystemFile AddFileToJob(SystemFile file)
        {
            BackupFiles.Add(file);
            file.JustFile.CopyTo(_subDirectoryPath);
            return file;
        }

        public IReadOnlyCollection<SystemFile> GetBackupFiles()
        {
            return BackupFiles.AsReadOnly();
        }
    }
}