namespace Dbscan.Test;

public static class DbscanTestData
{
	public class SimplePoint : IPointData
	{
		public SimplePoint(double x, double y) =>
			Point = new Point(x, y);

		public Point Point { get; }
	}

	internal static IList<SimplePoint> Borders = new List<SimplePoint>
	{
		new SimplePoint(x: 0, y: 0),
		new SimplePoint(x: -1.8, y: 0),
		new SimplePoint(x: -2.3, y: 0),
		new SimplePoint(x: -2.3, y: 0.5),
		new SimplePoint(x: -2.3, y: -0.5),
		new SimplePoint(x: 1.8, y: 0),
		new SimplePoint(x: 2.3, y: 0),
		new SimplePoint(x: 2.3, y: 0.5),
		new SimplePoint(x: 2.3, y: -0.5),
	};

	private static List<SimplePoint> BuildRingDataset()
	{
		var points = new List<SimplePoint>();

		var rows = 6;
		var cols = 5;
		for (var row = 0; row < rows; row++)
		{
			for (var col = 0; col < cols; col++)
			{
				var minPoints = row;
				var eps = 1.25 - col * 0.25;

				var x0 = -15 + (30 / (rows + 1)) * (row + 1);
				var y0 = -12 + (24 / (cols + 1)) * (col + 1);

				points.Add(new SimplePoint(x: x0, y: y0));
				for (var i = 0; i < minPoints; i++)
				{
					var x = x0 + eps * Math.Sin(2 * Math.PI * i / minPoints);
					var y = y0 + eps * Math.Cos(2 * Math.PI * i / minPoints);
					points.Add(new SimplePoint(x: x, y: y));
				}
			}
		}
		return points;
	}

	internal static IList<SimplePoint> RingDataset = BuildRingDataset();
}
