using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups.Entities.Algorithms;

public class Split : IAlgorithm
{
    public void MakeBackup(RestorePoint point, List<FileInfo> jobFiles, ISaver.ISaver saver)
    {
        var repoList = jobFiles.Select(file => new Repository().AddFiles(new List<FileInfo>() { file })).ToList();
        saver.Save(point, repoList);
    }
}