using System.Runtime.InteropServices;

namespace Dbscan;

/// <summary>
/// A point on the 2-d plane.
/// </summary>
/// <param name="X">The x-coordinate of the point.</param>
/// <param name="Y">The y-coordinate of the point.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Point(double X, double Y);
