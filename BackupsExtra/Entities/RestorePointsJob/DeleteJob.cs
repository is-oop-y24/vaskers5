using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using Backups.Entities.Algorithms;

namespace BackupsExtra.Entities.RestorePointsJob
{
    public class DeleteJob : IRestorePointsJob
    {
        public List<RestorePoint> DoJob(List<RestorePoint> allPoints, List<RestorePoint> invalidPoints)
        {
            return allPoints.Where(point => !invalidPoints.Contains(point)).ToList();
        }
    }
}