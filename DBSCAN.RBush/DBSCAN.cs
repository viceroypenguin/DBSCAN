namespace DBSCAN.RBush;

public static class DBSCANRBush
{
	public static ClusterSet<T> CalculateClusters<T>(
		IList<T> data,
		double epsilon,
		int minimumPointsPerCluster)
		where T : IPointData
	{
		var pointInfos = data
			.Select(p => new EnvelopePointInfo<T>(p))
			.ToList();

		return DBSCAN.CalculateClusters(
			new RBushSpatialIndex<EnvelopePointInfo<T>>(pointInfos),
			epsilon,
			minimumPointsPerCluster);
	}
}
