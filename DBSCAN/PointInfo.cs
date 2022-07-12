namespace DBSCAN;

public class PointInfo<T> : IPointData where T: IPointData
{
	public PointInfo(T item) => this.Item = item;

	public T Item { get; }
	public Cluster<T> Cluster { get; set; }
	public bool Visited { get; set; }

	public ref readonly Point Point => ref Item.Point;
}
