using RBush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSCAN.RBush
{
	public class RBushSpatialIndex<T> : ISpatialIndex<T>
		where T : ISpatialData, IPointData
	{
		public delegate double DistanceFunction(in Point a, in Point b);

		private RBush<T> tree;
		private DistanceFunction distanceFunction;

		public RBushSpatialIndex(IEnumerable<T> data)
			: this(data, EuclideanDistance) { }

		public RBushSpatialIndex(IEnumerable<T> data, DistanceFunction distanceFunction)
		{
			this.distanceFunction = distanceFunction;

			var tree = new RBush<T>();
			tree.BulkLoad(data);

			this.tree = tree;
		}

		public IReadOnlyList<T> Search() => this.tree.Search();

		public static double EuclideanDistance(in Point a, in Point b)
		{
			var xDist = b.X - a.X;
			var yDist = b.Y - a.Y;
			return Math.Sqrt(xDist * xDist + yDist * yDist);
		}

		public IReadOnlyList<T> Search(in Point p, double epsilon)
		{
			var rectangle = new Envelope(
				minX: p.X - epsilon,
				minY: p.Y - epsilon,
				maxX: p.X + epsilon,
				maxY: p.Y + epsilon);

			var l = new List<T>();
			foreach (var q in this.tree.Search(rectangle))
				if (distanceFunction(p, q.Point) < epsilon)
					l.Add(q);
			return l;
		}
	}
}
