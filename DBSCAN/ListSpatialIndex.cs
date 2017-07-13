using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSCAN
{
	public class ListSpatialIndex<T> : ISpatialIndex<T> where T : IPointData
	{
		private IReadOnlyList<T> list;
		private Func<Point, Point, double> distanceFunction;

		public ListSpatialIndex(IEnumerable<T> data)
			: this(data, EuclideanDistance) { }

		public ListSpatialIndex(IEnumerable<T> data, Func<Point, Point, double> distanceFunction)
		{
			this.list = data.ToList();
			this.distanceFunction = distanceFunction;
		}

		public static double EuclideanDistance(Point a, Point b)
		{
			var xDist = b.X - a.X;
			var yDist = b.Y - a.Y;
			return Math.Sqrt(xDist * xDist + yDist * yDist);
		}

		public IReadOnlyList<T> Search() => list;
		public IReadOnlyList<T> Search(Point p, double epsilon) =>
			list
				.Where(q => distanceFunction(p, q.Point) < epsilon)
				.ToList();
	}
}
