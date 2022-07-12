namespace DBSCAN;

public readonly struct Point
{
	public double X { get; }
	public double Y { get; }

	public Point(double X, double Y)
	{
		this.X = X;
		this.Y = Y;
	}
}
