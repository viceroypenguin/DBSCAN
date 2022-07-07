using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSCAN
{
	public static class DBSCAN
	{
		public static ClusterSet<T> CalculateClusters<T>(
			IList<T> data,
			double epsilon,
			int minimumPointsPerCluster)
			where T : IPointData
		{
			var pointInfos = data
				.Select(p => new PointInfo<T>(p))
				.ToList();

			return CalculateClusters(
				new ListSpatialIndex<PointInfo<T>>(pointInfos),
				epsilon,
				minimumPointsPerCluster);
		}

		public static ClusterSet<T> CalculateClusters<T>(
			ISpatialIndex<PointInfo<T>> index,
			double epsilon,
			int minimumPointsPerCluster)
			where T : IPointData
		{
			var points = index.Search().ToList();

			var clusters = new List<Cluster<T>>();

			foreach (var p in points)
			{
				if (p.Visited) continue;

				p.Visited = true;
				var candidates = index.Search(p, epsilon);

				if (candidates.Count >= minimumPointsPerCluster)
				{
					clusters.Add(
						BuildCluster(
							index,
							p,
							candidates,
							epsilon,
							minimumPointsPerCluster));
				}
			}

			return new ClusterSet<T>
			{
				Clusters = clusters,
				UnclusteredObjects = points
					.Where(p => p.Cluster == null)
					.Select(p => p.Item)
					.ToList(),
			};
		}

		private static Cluster<T> BuildCluster<T>(ISpatialIndex<PointInfo<T>> index, PointInfo<T> point, IReadOnlyList<PointInfo<T>> neighborhood, double epsilon, int minimumPointsPerCluster)
			where T : IPointData
		{
			var points = new List<T>() { point.Item };
			var cluster = new Cluster<T>() { Objects = points };
			point.Cluster = cluster;

			var queue = new Queue<PointInfo<T>>(neighborhood);
			while (queue.Any())
			{
				var newPoint = queue.Dequeue();
				if (!newPoint.Visited)
				{
					newPoint.Visited = true;
					var newNeighbors = index.Search(newPoint, epsilon);
					if (newNeighbors.Count >= minimumPointsPerCluster)
						foreach (var p in newNeighbors)
							queue.Enqueue(p);
				}

				if (newPoint.Cluster == null)
				{
					newPoint.Cluster = cluster;
					points.Add(newPoint.Item);
				}
			}

			return cluster;
		}

	}
}
