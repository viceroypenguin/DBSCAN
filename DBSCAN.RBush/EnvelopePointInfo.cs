using RBush;

namespace Dbscan.RBush;

/// <summary>
/// A holding class for algorithm related information about a point.
/// </summary>
/// <typeparam name="T">The type of the element this object is holding.</typeparam>
public class EnvelopePointInfo<T> : PointInfo<T>, ISpatialData
	where T : IPointData
{
	private readonly Envelope _envelope;

	/// <summary>
	/// Initializes a new <see cref="EnvelopePointInfo{T}"/> with the object it is holding.
	/// </summary>
	/// <param name="item"></param>
	public EnvelopePointInfo(T item) : base(item)
	{
		_envelope = new Envelope(
			MinX: item.Point.X,
			MinY: item.Point.Y,
			MaxX: item.Point.X,
			MaxY: item.Point.Y);
	}

	/// <inheritdoc />
	public ref readonly Envelope Envelope => ref _envelope;
}
