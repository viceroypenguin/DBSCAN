namespace Dbscan;

/// <summary>
/// Provides the base interface for the abstraction of
/// an index to find points.
/// </summary>
/// <typeparam name="T">The type of elements in the index.</typeparam>
public interface ISpatialIndex<out T>
{
	/// <summary>
	/// Get all of the elements within the current <see cref="ISpatialIndex{T}"/>.
	/// </summary>
	/// <returns>
	/// A list of every element contained in the <see cref="ISpatialIndex{T}"/>.
	/// </returns>
	IReadOnlyList<T> Search();

	/// <summary>
	/// Get all of the elements from this <see cref="ISpatialIndex{T}"/>
	/// within a circle centered at the point <see cref="IPointData.Point"/>
	/// with a radius of <paramref name="epsilon"/>.
	/// </summary>
	/// <param name="p">The center of the search circle.</param>
	/// <param name="epsilon">The radius of the search circle.</param>
	/// <returns>
	/// A list of the points that are within the search area.
	/// </returns>
	IReadOnlyList<T> Search(in IPointData p, double epsilon);
}
