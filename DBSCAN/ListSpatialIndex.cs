using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSCAN
{
	public class ListSpatialIndex<T> : ISpatialIndex<T> where T : IPointData
	{
		private IList<T> list;
		private Func<Point, Point, double> distanceFunction;

		public ListSpatialIndex(IList<T> data)
			: this(data, EuclideanDistance) { }

		public ListSpatialIndex(IList<T> data, Func<Point, Point, double> distanceFunction)
		{
			this.list = data;
			this.distanceFunction = distanceFunction;
		}

		public static double EuclideanDistance(Point a, Point b)
		{
			var xDist = b.X - a.X;
			var yDist = b.Y - a.Y;
			return Math.Sqrt(xDist * xDist + yDist * yDist);
		}

		public IList<T> Search() => list;
		public IList<T> Search(Point p, double epsilon) =>
			list
				.Where(q => distanceFunction(p, q.Point) < epsilon)
				.ToList();
	}
}
