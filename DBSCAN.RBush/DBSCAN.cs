namespace Dbscan.RBush;

/// <summary>
/// Contains static methods to run the DBSCAN algorithm using the RBush spatial index.
/// </summary>
public static class DbscanRBush
{
	/// <summary>
	/// Run the DBSCAN algorithm on a collection of points, using the <see cref="RBushSpatialIndex{T}"/> index.
	/// </summary>
	/// <typeparam name="T">The type of elements to cluster.</typeparam>
	/// <param name="data">The collection of elements to cluster.</param>
	/// <param name="epsilon">The epsilon parameter to use in the algorithm; used to determine the radius of the circle to find neighboring points.</param>
	/// <param name="minimumPointsPerCluster">The minimum number of points required to create a cluster or to add additional points to the cluster.</param>
	/// <returns>A <see cref="ClusterSet{T}"/> containing the list of <see cref="Cluster{T}"/>s and a list of unclustered points.</returns>
	/// <remarks>This method is an O(N log N) operation, where N is the Length of the dataset</remarks>
	public static ClusterSet<T> CalculateClusters<T>(
		IEnumerable<T> data,
		double epsilon,
		int minimumPointsPerCluster)
		where T : IPointData
	{
		var pointInfos = data
			.Select(p => new EnvelopePointInfo<T>(p))
			.ToList();

		return Dbscan.CalculateClusters(
			new RBushSpatialIndex<EnvelopePointInfo<T>>(pointInfos),
			epsilon,
			minimumPointsPerCluster);
	}
}
