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
		private RBush<T> tree;
		private Func<Point, Point, double> distanceFunction;

		public RBushSpatialIndex(IEnumerable<T> data)
			: this(data, EuclideanDistance) { }

		public RBushSpatialIndex(IEnumerable<T> data, Func<Point, Point, double> distanceFunction)
		{
			this.distanceFunction = distanceFunction;

			var tree = new RBush<T>();
			tree.BulkLoad(data);

			this.tree = tree;
		}

		public IReadOnlyList<T> Search() => this.tree.Search().ToList();

		public static double EuclideanDistance(Point a, Point b)
		{
			var xDist = b.X - a.X;
			var yDist = b.Y - a.Y;
			return Math.Sqrt(xDist * xDist + yDist * yDist);
		}

		public IReadOnlyList<T> Search(Point p, double epsilon)
		{
			var rectangle = new Envelope
			{
				MinX = p.X - epsilon,
				MinY = p.Y - epsilon,
				MaxX = p.X + epsilon,
				MaxY = p.Y + epsilon,
			};

			return this.tree.Search(rectangle)
				.Where(q => distanceFunction(p, q.Point) < epsilon)
				.ToList();
		}
	}
}
