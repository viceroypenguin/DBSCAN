using System;

namespace DBSCAN
{
	public class PointInfo<T> : IPointData where T: IPointData
	{
		public PointInfo(T item) => this.Item = item;

		public T Item { get; set; }
		public Cluster<T> Cluster { get; set; }
		public bool Visited { get; set; }

		public Point Point => Item.Point;
	}
}
