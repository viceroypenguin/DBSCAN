using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DBSCAN.Test
{
	public class DbscanTests
	{
		#region Border Data Set
		static List<Point> Borders = new List<Point>
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

		private ISpatialIndex<PointInfo<Point>> GetBorderIndex()
		{
			var pointInfos = Borders
				.Select(p => new PointInfo<Point>(p))
				.ToList();
			return new ListSpatialIndex<PointInfo<Point>>(pointInfos);
		}

		[Fact]
		public void BorderTest1()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetBorderIndex(),
					1.0,
					4);

			Assert.Equal(2, clusters.Clusters.Count);
			Assert.Equal(1, clusters.UnclusteredObjects.Count);
			Assert.Equal(Borders[0], clusters.UnclusteredObjects[0]);

			Assert.Equal(4, clusters.Clusters[0].Objects.Count);
			Assert.Equal(4, clusters.Clusters[1].Objects.Count);
		}

		[Fact]
		public void BorderTest2()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetBorderIndex(),
					2.0,
					3);

			Assert.Equal(1, clusters.Clusters.Count);
			Assert.Equal(0, clusters.UnclusteredObjects.Count);
			Assert.Equal(9, clusters.Clusters[0].Objects.Count);
		}

		[Fact]
		public void BorderTest3()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetBorderIndex(),
					2.0,
					4);

			Assert.Equal(2, clusters.Clusters.Count);
			Assert.Equal(0, clusters.UnclusteredObjects.Count);

			Assert.Equal(5, clusters.Clusters[0].Objects.Count);
			Assert.Equal(4, clusters.Clusters[1].Objects.Count);
		}

		[Fact]
		public void BorderTest4()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetBorderIndex(),
					2.0,
					5);

			Assert.Equal(2, clusters.Clusters.Count);
			Assert.Equal(0, clusters.UnclusteredObjects.Count);

			Assert.Equal(5, clusters.Clusters[0].Objects.Count);
			Assert.Equal(4, clusters.Clusters[1].Objects.Count);
		}
		#endregion

		#region Ring Data Set
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

		static List<Point> RingDataset = BuildRingDataset();
		private ISpatialIndex<PointInfo<Point>> GetRingIndex()
		{
			var pointInfos = RingDataset
				.Select(p => new PointInfo<Point>(p))
				.ToList();
			return new ListSpatialIndex<PointInfo<Point>>(pointInfos);
		}

		[Fact]
		public void RingTest1()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetRingIndex(),
					1.0,
					4);

			Assert.Equal(9, clusters.Clusters.Count);
			Assert.Equal(60, clusters.UnclusteredObjects.Count);
		}

		[Fact]
		public void RingTest2()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetRingIndex(),
					1.01,
					4);

			Assert.Equal(12, clusters.Clusters.Count);
			Assert.Equal(45, clusters.UnclusteredObjects.Count);
		}

		[Fact]
		public void RingTest3()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetRingIndex(),
					1.3,
					4);

			Assert.Equal(15, clusters.Clusters.Count);
			Assert.Equal(30, clusters.UnclusteredObjects.Count);
		}

		[Fact]
		public void RingTest4()
		{
			var clusters =
				DBSCAN.CalculateClusters(
					GetRingIndex(),
					0.99,
					2);

			Assert.Equal(15, clusters.Clusters.Count);
			Assert.Equal(45, clusters.UnclusteredObjects.Count);
		}
		#endregion
	}
}
