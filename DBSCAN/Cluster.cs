namespace Dbscan;

/// <summary>
/// A collection of items that have been clustered by the algorithm.
/// </summary>
/// <typeparam name="T">The type of elements in the cluster.</typeparam>
public class Cluster<T>
{
	/// <summary>
	/// The items that have been clustered.
	/// </summary>
	public IReadOnlyList<T> Objects { get; internal set; } = default!;
}
