using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Entities;
using Backups.Entities.Algorithms;
using Backups.Entities.ISaver;
using Backups.Services;
using Backups.Tools;
using BackupsExtra.Entities.RestorePointsCleaningAlgorithms;
using BackupsExtra.Entities.RestorePointsJob;
using BackupsExtra.Services;
using BackupsExtra.Tools;
using NUnit.Framework;
using Single = Backups.Entities.Algorithms.Single;

namespace BackupsExtra.Tests
{
    public class Tests
    {
        private BackupExtraService _backupsService;
        private IAlgorithm _algoSplit;
        private IAlgorithm _algoSingle;
        private ISaver _virtualSaver;
        private IRestorePointsJob _deleteJob;
        private IRestorePointsJob _mergeJob;
        private IClearAlgorithm _dateClear;
        private IClearAlgorithm _countClear;
        private IClearAlgorithm _gibridClear;


        [SetUp]
        public void Setup()
        {
            _backupsService = new BackupExtraService();
            _algoSplit = new Split();
            _algoSingle = new Single();
            _virtualSaver = new VirtualSaver();
            _deleteJob = new DeleteJob();
            _mergeJob = new MergeJob();
            _dateClear = new ClearByDateAlgorithm(DateTime.Now);
            _countClear = new ClearByNumberAlgorithm(3);
            _gibridClear = new GibridAlgorithm(3, DateTime.Now);
        }

        [Test]
        public void CheckJobCreation_VirtualTest()
        {
            string testDirectoryPath = @"/home/vaskers5/projects/oop/Backups/TesDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver, _countClear, _deleteJob);
            var job2 = _backupsService.AddNewJob(testDirectoryPath + "/job-2", _algoSplit, _virtualSaver, _countClear, _deleteJob);
            Assert.AreEqual(_backupsService.BackupJobs.Count, 2);
        }
        
        [Test]
        public void Check_SingleFileCreation_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSingle, _virtualSaver, _countClear, _deleteJob);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            job1.CreateNewRestorePoint();
            job1.CreateNewRestorePoint();
            job1.CreateNewRestorePoint();
            job1.CreateNewRestorePoint();
            Assert.AreEqual(job1.RestorePoints.Count, 3);
            
        }
        
        [Test]
        public void Check_SplitFileCreation_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver, _countClear, _mergeJob);
            
            var f1 = _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            var f2 = _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            var f3 = _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            job1.CreateNewRestorePoint();
            job1.RemoveFileFromJob(f1);
            job1.CreateNewRestorePoint();
            job1.CreateNewRestorePoint();
            var lastPoint = job1.CreateNewRestorePoint();
            Assert.Contains(f1, lastPoint.Archives[2].RepoFiles);
            Assert.AreEqual(job1.RestorePoints.Count, 3);
            
        }
        
        [Test]
        public void Check_JobContainsFiles_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver, _countClear, _deleteJob);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            Assert.AreEqual(job1.GetBackupFiles().Count, 3);
        }
        
        [Test]
        public void Check_JobCreateRestorePointWithError()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver, new ClearByNumberAlgorithm(0), _deleteJob);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            Assert.Catch<BackupsExtraException>(() =>
            {
                job1.CreateNewRestorePoint();
            });
        }
    }
}