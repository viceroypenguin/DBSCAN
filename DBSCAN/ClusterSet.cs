namespace Dbscan;

/// <summary>
/// The result of running the DBSCAN algorithm on a set of data.
/// </summary>
/// <typeparam name="T">The type of elements in the cluster.</typeparam>
public class ClusterSet<T>
{
	/// <summary>
	/// A list of the clusters that have been identified.
	/// </summary>
	public IReadOnlyList<Cluster<T>> Clusters { get; internal init; } = default!;

	/// <summary>
	/// A list of the items that were not identified as being part of a cluster.
	/// </summary>
	public IReadOnlyList<T> UnclusteredObjects { get; internal init; } = default!;
}
