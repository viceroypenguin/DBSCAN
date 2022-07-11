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
				MinX: item.Point.X,
				MinY: item.Point.Y,
				MaxX: item.Point.X,
				MaxY: item.Point.Y);
		}

		public ref readonly Envelope Envelope => ref _envelope;
	}
}
