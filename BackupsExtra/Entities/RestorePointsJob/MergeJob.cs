using System.Collections.Generic;
using Backups.Entities;
using Backups.Entities.Algorithms;
using BackupsExtra.Entities.RestorePointsCleaningAlgorithms;

namespace BackupsExtra.Entities.RestorePointsJob
{
    public class MergeJob : IRestorePointsJob
    {
        public List<RestorePoint> DoJob(List<RestorePoint> allPoints, List<RestorePoint> invalidPoints)
        {
            foreach (var point in invalidPoints)
            {
                if (point.Archives.Count > 1)
                {
                    foreach (var repo in point.Archives)
                    {
                        if (!allPoints[^1].Archives.Contains(repo))
                            allPoints[^1].Archives.Add(repo);
                    }
                }

                allPoints.Remove(point);
            }

            return allPoints;
        }
    }
}