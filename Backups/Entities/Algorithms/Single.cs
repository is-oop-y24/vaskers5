using System.Collections.Generic;
using System.IO;

namespace Backups.Entities.Algorithms;

public class Single : IAlgorithm
{
    public void MakeBackup(RestorePoint point, List<FileInfo> jobFiles, ISaver.ISaver saver)
    {
        var repoList = new List<Repository>() { new Repository().AddFiles(jobFiles) };
        saver.Save(point, repoList);
    }
}