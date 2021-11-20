using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private BackupsService _backupsService;

        [SetUp]
        public void Setup()
        {
            _backupsService = new BackupsService();
        }

        [Test]
        public void CheckJobCreation_VirtualTest()
        {
            string testDirectoryPath = @"/home/vaskers5/projects/oop/Backups/TesDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Split, true);
            var job2 = _backupsService.AddNewJob(testDirectoryPath + "/job-2", BackupAlgorithms.Split, true);
            Assert.AreEqual(_backupsService.BackupJobs.Count, 2);
        }
        
        [Test]
        public void Check_SingleFileCreation_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Single, true);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            job1.CreateNewVirtualRestorePoint();
            Assert.AreEqual(job1.RestorePoints.Count, 1);
        }
        
        [Test]
        public void Check_SplitFileCreation_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Split, true);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            job1.CreateNewVirtualRestorePoint();
            Assert.AreEqual(job1.RestorePoints.Count, 1);
        }
        
        [Test]
        public void Check_JobContainsFiles_VirtualTest()
        {
            string testDirectoryPath = "/home/vaskers5/projects/oop/Backups/TestDirectory";
            var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Split, true);
            
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
            _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
            
            Assert.AreEqual(job1.GetBackupFiles().Count, 3);
        }


        // [Test]
        // public void Check_SingleFileCreation_LocalTest()
        // {
        //     string testDirectoryPath = @"/home/vaskers5/projects/oop/Backups/TestDirectory";
        //     var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Split, false);
        //
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
        //     job1.CreateNewRestorePoint();
        //     Assert.AreEqual(job1.RestorePoints.Count, 1);
        //     Assert.AreEqual(job1.RestorePoints[0].Archives.Count, 1);
        // }
        
        // [Test]
        // public void Check_SplitFileCreation_LocalTest()
        // {
        //     string testDirectoryPath = @"/home/vaskers5/projects/oop/Backups/TestDirectory";
        //     var job1 = _backupsService.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Split, false);
        //
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + "/a.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + "/b.txt");
        //     _backupsService.AddFileToJob(job1, testDirectoryPath + "/c.txt");
        //     job1.CreateNewRestorePoint();
        //     Assert.AreEqual(job1.RestorePoints.Count, 1);
        //     Assert.AreEqual(job1.RestorePoints[0].Archives.Count, 3);
        // }
    }
}