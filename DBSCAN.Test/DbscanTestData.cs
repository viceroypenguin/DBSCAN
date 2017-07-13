using System;
using System.Collections.Generic;
using System.Text;

namespace DBSCAN.Test
{
    public static class DbscanTestData
	{
		public static IList<Point> Borders = new List<Point>
		{
			new Point { X = 0, Y = 0, },
			new Point { X = -1.8, Y = 0, },
			new Point { X = -2.3, Y = 0, },
			new Point { X = -2.3, Y = 0.5, },
			new Point { X = -2.3, Y = -0.5, },
			new Point { X = 1.8, Y = 0, },
			new Point { X = 2.3, Y = 0, },
			new Point { X = 2.3, Y = 0.5, },
			new Point { X = 2.3, Y = -0.5, },
		};

		private static List<Point> BuildRingDataset()
		{
			var points = new List<Point>();

			var rows = 6;
			var cols = 5;
			for (var row = 0; row < rows; row++)
			{
				for (var col = 0; col < cols; col++)
				{
					var MinPoints = row;
					var eps = 1.25 - col * 0.25;

					var x0 = -15 + (30 / (rows + 1)) * (row + 1);
					var y0 = -12 + (24 / (cols + 1)) * (col + 1);

					points.Add(new Point { X = x0, Y = y0, });
					for (var i = 0; i < MinPoints; i++)
					{
						var x = x0 + eps * Math.Sin(2 * Math.PI * i / MinPoints);
						var y = y0 + eps * Math.Cos(2 * Math.PI * i / MinPoints);
						points.Add(new Point { X = x, Y = y, });
					}
				}
			}
			return points;
		}

		public static IList<Point> RingDataset = BuildRingDataset();
	}
}
