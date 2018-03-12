using RBush;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBSCAN.RBush
{
	public class EnvelopePointInfo<T> : PointInfo<T>, ISpatialData
		where T: IPointData
	{
		private readonly Envelope _envelope;

		public EnvelopePointInfo(T item) : base(item)
		{
			_envelope = new Envelope(
				minX: item.Point.X,
				minY: item.Point.Y,
				maxX: item.Point.X,
				maxY: item.Point.Y);
		}

		public ref readonly Envelope Envelope => ref _envelope;
	}
}
