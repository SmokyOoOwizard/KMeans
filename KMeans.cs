using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KMeans
{
    public class KMeans
    {
        public VectorN[] Data;
        public VectorN[] clusters;
        public int ClustersCount;
        public int Seed;

        public KMeans(VectorN[] data, int clustersCount, int Seed)
        {
            Data = data;
            ClustersCount = clustersCount;
            clusters = new VectorN[ClustersCount];
            this.Seed = Seed;
        }
        void Clustering()
        {
            Dictionary<VectorN, List<VectorN>> centerAssignments = GetCenterAssignments(Data, PickRandomCenters(ClustersCount, Data));

            List<VectorN> oldCenters = null;
            while (true)
            {
                List<VectorN> newCenters = GetNewCenters(centerAssignments);

                if (CentersEqual(newCenters, oldCenters))
                {
                    break;
                }

                centerAssignments = GetCenterAssignments(Data, newCenters);

                oldCenters = newCenters;
            }
        }
        private bool CentersEqual(List<VectorN> newCenters, List<VectorN> oldCenters)
        {
            if (newCenters == null || oldCenters == null)
            {
                return false;
            }

            foreach (VectorN newCenter in newCenters)
            {
                bool found = false;
                foreach (VectorN oldCenter in oldCenters)
                {
                    if (newCenter.Equals(oldCenter))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }
            return true;
        }
        private List<VectorN> GetNewCenters(Dictionary<VectorN, List<VectorN>> centerAssignments)
        {
            VectorN total = new VectorN();

            var newCenters = new List<VectorN>();

            foreach (VectorN center in centerAssignments.Keys)
            {
                total = new VectorN();

                foreach (VectorN point in centerAssignments[center])
                {
                    total += point;
                }

                VectorN average = total / centerAssignments[center].Count;

                newCenters.Add(average);
            }
            return newCenters;
        }
        private Dictionary<VectorN, List<VectorN>> GetCenterAssignments(VectorN[] points, List<VectorN> centers)
        {
            var centerAssignments = new Dictionary<VectorN, List<VectorN>>();

            foreach (VectorN point in centers)
            {
                centerAssignments.Add(point, new List<VectorN>());
            }


            foreach (VectorN point in points)
            {
                VectorN closestCenter = new VectorN();
                double closestCenterDistance = double.MaxValue;

                foreach (VectorN centerPoint in centers)
                {

                    float distance = VectorN.Distance(point, centerPoint);

                    if (distance < closestCenterDistance)
                    {
                        closestCenterDistance = distance;
                        closestCenter = centerPoint;
                    }
                }

                lock (centerAssignments)
                {
                    centerAssignments[closestCenter].Add(point);
                }
            }

            return centerAssignments;
        }
        private List<VectorN> PickRandomCenters(int clusters, VectorN[] points)
        {
            System.Random r = new System.Random(Seed);
            var randomCenters = new List<VectorN>();
            int pickedPointCount = 0;
            while (pickedPointCount < clusters)
            {
                var point = points[r.Next(0, points.Length - 1)];
                if (!randomCenters.Contains(point))
                {
                    randomCenters.Add(point);
                    pickedPointCount++;
                }
            }
            return randomCenters;
        }
    }
}
