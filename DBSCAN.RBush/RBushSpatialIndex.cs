using RBush;

namespace Dbscan.RBush;

public class RBushSpatialIndex<T> : ISpatialIndex<T>
	where T : ISpatialData, IPointData
{
	public delegate double DistanceFunction(in IPointData a, in IPointData b);

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

	public static double EuclideanDistance(in IPointData a, in IPointData b)
	{
		var xDist = b.Point.X - a.Point.X;
		var yDist = b.Point.Y - a.Point.Y;
		return Math.Sqrt(xDist * xDist + yDist * yDist);
	}

	public IReadOnlyList<T> Search(in IPointData p, double epsilon)
	{
		var rectangle = new Envelope(
			MinX: p.Point.X - epsilon,
			MinY: p.Point.Y - epsilon,
			MaxX: p.Point.X + epsilon,
			MaxY: p.Point.Y + epsilon);

		var l = new List<T>();
		foreach (var q in this.tree.Search(rectangle))
			if (distanceFunction(p, q) < epsilon)
				l.Add(q);
		return l;
	}
}
