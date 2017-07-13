using RBush;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBSCAN.RBush
{
	public class EnvelopePointInfo<T> : PointInfo<T>, ISpatialData
		where T: IPointData
	{
		public EnvelopePointInfo() { }
		public EnvelopePointInfo(T item) : base(item) { }

		public Envelope Envelope => new Envelope
		{
			MinX = Item.Point.X,
			MinY = Item.Point.Y,
			MaxX = Item.Point.X,
			MaxY = Item.Point.Y,
		};
	}
}
