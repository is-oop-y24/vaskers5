using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;

namespace BackupsExtra.Entities.RestorePointsCleaningAlgorithms
{
    public class GibridAlgorithm : IClearAlgorithm
    {
        public GibridAlgorithm(int countLimit, DateTime dateLimit)
        {
            CountLimit = countLimit;
            DateLimit = dateLimit;
        }

        public int CountLimit { get; set; }

        public DateTime DateLimit { get; }

        public List<RestorePoint> MakeClear(List<RestorePoint> points)
        {
            List<RestorePoint> validPoints = points.Count > CountLimit ? points.Take(points.Count - CountLimit).ToList() : new List<RestorePoint>();
            validPoints = validPoints.Where(point => point.CreationTime < DateLimit).ToList();
            return validPoints;
        }
    }
}