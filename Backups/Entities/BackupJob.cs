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
        public BackupJob(string jobPath, BackupAlgorithms algorithm, List<SystemFile> files, bool isVirtual)
        {
            JobPath = jobPath;
            Algorithm = algorithm;
            SubDirectoryPath = jobPath + "/" + "SubDirectory";
            IsVirtual = isVirtual;
            if (!IsVirtual)
            {
                Directory.CreateDirectory(jobPath);
                Directory.CreateDirectory(SubDirectoryPath);
            }

            BackupFiles = files;
            RestorePoints = new List<RestorePoint>() { };
        }

        public BackupJob(string jobPath, BackupAlgorithms algorithm, bool isVirtual)
            : this(jobPath, algorithm, new List<SystemFile>() { }, isVirtual)
        {
        }

        public string JobPath { get; set; }
        public BackupAlgorithms Algorithm { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }

        public bool IsVirtual { get; }
        private string SubDirectoryPath { get; }
        private List<SystemFile> BackupFiles { get; set; }

        public virtual RestorePoint CreateNewRestorePoint()
        {
            string restorePointPath = JobPath + "/RestorePoint_" + _lastId++;
            BackupFiles.Select(file => file.JustFile.CopyTo(SubDirectoryPath));
            Directory.CreateDirectory(restorePointPath);
            RestorePoint restorePoint;
            switch (Algorithm)
            {
                case BackupAlgorithms.Split:
                    var archives = (from file in BackupFiles
                        let archPath = restorePointPath + "/" + file.JustFile.Name + ".zip"
                        select ZipFile.Open(archPath, ZipArchiveMode.Create)
                            .CreateEntryFromFile(file.JustFile.FullName, archPath)
                            .Archive).ToList();

                    restorePoint = new RestorePoint(_lastId, restorePointPath,  archives);
                    break;
                case BackupAlgorithms.Single:
                    string zipArchivePath = restorePointPath + "/" + _lastId + ".zip";
                    ZipFile.CreateFromDirectory(SubDirectoryPath, zipArchivePath);
                    ZipArchive archive = ZipFile.Open(zipArchivePath, ZipArchiveMode.Read);
                    restorePoint = new RestorePoint(_lastId, restorePointPath, archive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            RestorePoints.Add(restorePoint);
            return restorePoint;
        }

        public virtual RestorePoint CreateNewVirtualRestorePoint()
        {
            string restorePointPath = JobPath + "/RestorePoint_" + _lastId++;
            RestorePoint restorePoint;
            switch (Algorithm)
            {
                case BackupAlgorithms.Split:
                    var archives = new List<ZipArchive>() { };
                    restorePoint = new RestorePoint(_lastId, restorePointPath,  archives);
                    break;
                case BackupAlgorithms.Single:
                    restorePoint = new RestorePoint(_lastId, restorePointPath, new List<ZipArchive>() { });
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
            return Directory.GetFiles(SubDirectoryPath).FirstOrDefault(filePath => filePath == file.JustFile.Name);
        }

        public SystemFile RemoveFileFromJob(SystemFile file)
        {
            BackupFiles.Remove(file);
            if (IsVirtual) return file;

            var jobFile = new FileInfo(FindFilePathInJob(file));
            if (jobFile is null)
                throw new FileNotExistException("This file is not exist at current job");
            jobFile.Delete();

            return file;
        }

        public SystemFile AddFileToJob(SystemFile file)
        {
            BackupFiles.Add(file);
            if (IsVirtual) return file;
            file.JustFile.CopyTo(SubDirectoryPath + "/" + file.JustFile.Name);
            return file;
        }

        public IReadOnlyCollection<SystemFile> GetBackupFiles()
        {
            return BackupFiles.AsReadOnly();
        }
    }
}