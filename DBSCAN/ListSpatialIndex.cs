namespace Dbscan;

/// <summary>
/// Represents a method that calculates the distance between two <see cref="IPointData"/> objects.
/// </summary>
/// <param name="p1">An object representing the first point.</param>
/// <param name="p2">An object representing the second point.</param>
/// <returns>The distance between points <paramref name="p1"/> and <paramref name="p2"/>.</returns>
public delegate double DistanceFunction(in IPointData p1, in IPointData p2);

internal static class DistanceFunctions
{
	public static double EuclideanDistance(in IPointData a, in IPointData b)
	{
		var xDist = b.Point.X - a.Point.X;
		var yDist = b.Point.Y - a.Point.Y;
		return Math.Sqrt(xDist * xDist + yDist * yDist);
	}
}

/// <summary>
/// An implementation of the <see cref="ISpatialIndex{T}"/> using a simple <see cref="List{T}"/>
/// to hold the elements and a linear search of all items to <see cref="Search(in IPointData, double)"/>
/// for nearby items.
/// </summary>
/// <typeparam name="T">The type of items in the index.</typeparam>
public class ListSpatialIndex<T> : ISpatialIndex<T> where T : IPointData
{
	private readonly IReadOnlyList<T> _list;
	private readonly DistanceFunction _distanceFunction;

	/// <summary>
	/// Initializes a <see cref="ListSpatialIndex{T}"/> with a collection of data, using the
	/// Euclidean distance between two points as the distance function to search for points 
	/// in a given neighborhood.
	/// </summary>
	/// <param name="data">The collection of data to put into the index</param>
	public ListSpatialIndex(IEnumerable<T> data)
		: this(data, DistanceFunctions.EuclideanDistance) { }

	/// <summary>
	/// Initializes a <see cref="ListSpatialIndex{T}"/> with a collection of data, using the
	/// specified distanct function to search for points in a given neighborhood.
	/// </summary>
	/// <param name="data">The collection of data to put into the index</param>
	/// <param name="distanceFunction">The function used to determine if a point is within a specified distance of a given point.</param>
	public ListSpatialIndex(IEnumerable<T> data, DistanceFunction distanceFunction)
	{
		_list = data.ToList();
		_distanceFunction = distanceFunction;
	}

	/// <summary>
	/// Get all of the elements within the current <see cref="ListSpatialIndex{T}"/>.
	/// </summary>
	/// <returns>
	/// A list of every element contained in the <see cref="ListSpatialIndex{T}"/>.
	/// </returns>
	public IReadOnlyList<T> Search() => _list;

	/// <summary>
	/// Get all of the elements from this <see cref="ListSpatialIndex{T}"/>
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
		var l = new List<T>();
		foreach (var q in _list)
			if (_distanceFunction(p, q) < epsilon)
				l.Add(q);
		return l;
	}
}
