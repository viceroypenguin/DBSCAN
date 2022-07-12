namespace DBSCAN;

public class ClusterSet<T>
    {
	public IList<Cluster<T>> Clusters { get; internal set; }
	public IList<T> UnclusteredObjects { get; internal set; }
    }
