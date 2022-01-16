using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.Algorithms;

namespace BackupsExtra.Entities.RestorePointsJob
{
    public interface IRestorePointsJob
    {
        List<RestorePoint> DoJob(List<RestorePoint> allPoints, List<RestorePoint> invalidPoints);
    }
}