namespace DBSCAN;

public interface ISpatialIndex<out T>
{
	IReadOnlyList<T> Search();
	IReadOnlyList<T> Search(in IPointData p, double epsilon);
}
