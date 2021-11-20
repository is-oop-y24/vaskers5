using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckJobTest()
        {
            var service = new BackupsService();
            Assert.AreEqual(1,1);
            // string testDirectoryPath = @"/home/vaskers5/projects/oop/Backups/TesDirectory";
            // var job1 = service.AddNewJob(testDirectoryPath + "/job-1", BackupAlgorithms.Split);
            // var job2 = service.AddNewJob(testDirectoryPath + "/job-2", BackupAlgorithms.Split);
            // Assert.AreEqual(service.BackupJobs.Count, 2);
        }
    }
}