namespace Dbscan;

/// <summary>
/// Exposes a <see cref="Point"/> that identifies where an object is.
/// </summary>
public interface IPointData
{
	/// <summary>
	/// The location of the current object.
	/// </summary>
	Point Point { get; }
}
