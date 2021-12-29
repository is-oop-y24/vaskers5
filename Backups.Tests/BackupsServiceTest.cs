using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.Algorithms;
using Backups.Entities.ISaver;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private BackupsService _backupsService;
        private IAlgorithm _algoSplit;
        private IAlgorithm _algoSingle;
        private ISaver _virtualSaver;

        [SetUp]
        public void Setup()
        {
            _backupsService = new BackupsService();
            _algoSplit = new Split();
            _algoSingle = new Single();
            _virtualSaver = new VirtualSaver();
        }

        [Test]
        public void CheckJobCreation_VirtualTest()
        {
            string testDirectoryPath = @"/home/vaskers5/projects/oop/Backups/TesDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver);
            var job2 = _backupsService.AddNewJob(testDirectoryPath + "/job-2", _algoSplit, _virtualSaver);
            Assert.AreEqual(_backupsService.BackupJobs.Count, 2);
        }
        
        [Test]
        public void Check_SingleFileCreation_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSingle, _virtualSaver);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            job1.CreateNewRestorePoint();
            Assert.AreEqual(job1.RestorePoints.Count, 1);
        }
        
        [Test]
        public void Check_SplitFileCreation_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            job1.CreateNewRestorePoint();
            Assert.AreEqual(job1.RestorePoints.Count, 1);
        }
        
        [Test]
        public void Check_JobContainsFiles_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSplit, _virtualSaver);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            
            Assert.AreEqual(job1.GetBackupFiles().Count, 3);
        }


        // [Test]
        // public void Check_SingleFileCreation_LocalTest()
        // {
        //     string testDirectoryPath = @"C:\Users\danil\RiderProjects\vaskers5\Backups\TestDirectory";
        //     var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", _algoSingle, new LocalSaver());
        //
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + @"\a.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + @"\b.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + @"\c.txt");
        //     job1.CreateNewRestorePoint();
        //     Assert.AreEqual(job1.RestorePoints.Count, 1);
        //     Assert.AreEqual(job1.RestorePoints[0].Archives.Count, 1);
        // }
        //
        // [Test]
        // public void Check_SplitFileCreation_LocalTest()
        // {
        //     string testDirectoryPath = @"C:\Users\danil\RiderProjects\vaskers5\Backups\TestDirectory";
        //     var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-2", _algoSplit, new LocalSaver());
        //
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + @"\a.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + @"\b.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + @"\c.txt");
        //     job1.CreateNewRestorePoint();
        //     Assert.AreEqual(job1.RestorePoints.Count, 1);
        //     Assert.AreEqual(job1.RestorePoints[0].Archives.Count, 3);
        // }
    }
}