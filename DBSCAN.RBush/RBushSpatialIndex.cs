using RBush;

namespace Dbscan.RBush;

/// <summary>
/// An implementation of the <see cref="ISpatialIndex{T}"/> using a simple <see cref="List{T}"/>
/// to hold the elements and a linear search of all items to <see cref="Search(in IPointData, double)"/>
/// for nearby items.
/// </summary>
/// <typeparam name="T">The type of items in the index.</typeparam>
public class RBushSpatialIndex<T> : ISpatialIndex<T>
	where T : ISpatialData, IPointData
{
	private readonly RBush<T> _tree;

	/// <summary>
	/// Initializes a <see cref="ListSpatialIndex{T}"/> with a collection of data, using the
	/// Euclidean distance between two points as the distance function to search for points 
	/// in a given neighborhood.
	/// </summary>
	/// <param name="data">The collection of data to put into the index</param>
	public RBushSpatialIndex(IEnumerable<T> data)
	{
		var tree = new RBush<T>();
		tree.BulkLoad(data);
		_tree = tree;
	}

	/// <summary>
	/// Get all of the elements within the current <see cref="RBushSpatialIndex{T}"/>.
	/// </summary>
	/// <returns>
	/// A list of every element contained in the <see cref="RBushSpatialIndex{T}"/>.
	/// </returns>
	public IReadOnlyList<T> Search() => this._tree.Search();

	private static double EuclideanDistance(in IPointData a, in IPointData b)
	{
		var xDist = b.Point.X - a.Point.X;
		var yDist = b.Point.Y - a.Point.Y;
		return Math.Sqrt(xDist * xDist + yDist * yDist);
	}

	/// <summary>
	/// Get all of the elements from this <see cref="RBushSpatialIndex{T}"/>
	/// within a circle centered at the point <see cref="IPointData.Point"/>
	/// with a radius of <paramref name="epsilon"/>.
	/// </summary>
	/// <param name="p">The center of the search circle.</param>
	/// <param name="epsilon">The radius of the search circle.</param>
	/// <returns>
	/// A list of the points that are within the search area.
	/// </returns>
	public IReadOnlyList<T> Search(in IPointData p, double epsilon)
	{
		var rectangle = new Envelope(
			MinX: p.Point.X - epsilon,
			MinY: p.Point.Y - epsilon,
			MaxX: p.Point.X + epsilon,
			MaxY: p.Point.Y + epsilon);

		var l = new List<T>();
		foreach (var q in _tree.Search(rectangle))
			if (EuclideanDistance(p, q) < epsilon)
				l.Add(q);
		return l;
	}
}
