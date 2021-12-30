using System.Collections.Generic;
using System.IO;
using Backups.Entities.ISaver;

namespace Backups.Entities.Algorithms;

public interface IAlgorithm
{
    void MakeBackup(RestorePoint point, List<FileInfo> jobFiles, ISaver.ISaver saver);
}