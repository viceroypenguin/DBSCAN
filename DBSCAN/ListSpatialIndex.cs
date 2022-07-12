namespace Dbscan;

public class ListSpatialIndex<T> : ISpatialIndex<T> where T : IPointData
{
	public delegate double DistanceFunction(in IPointData a, in IPointData b);

	private IReadOnlyList<T> list;
	private DistanceFunction distanceFunction;

	public ListSpatialIndex(IEnumerable<T> data)
		: this(data, EuclideanDistance) { }

	public ListSpatialIndex(IEnumerable<T> data, DistanceFunction distanceFunction)
	{
		this.list = data.ToList();
		this.distanceFunction = distanceFunction;
	}

	public static double EuclideanDistance(in IPointData a, in IPointData b)
	{
		var xDist = b.Point.X - a.Point.X;
		var yDist = b.Point.Y - a.Point.Y;
		return Math.Sqrt(xDist * xDist + yDist * yDist);
	}

	public IReadOnlyList<T> Search() => list;
	public IReadOnlyList<T> Search(in IPointData p, double epsilon)
	{
		var l = new List<T>();
		foreach (var q in list)
			if (distanceFunction(p, q) < epsilon)
				l.Add(q);
		return l;
	}
}
