using System;
using System.Collections.Generic;
using System.Text;

namespace DBSCAN
{
	public interface ISpatialIndex<out T>
	{
		IReadOnlyList<T> Search();
		IReadOnlyList<T> Search(in Point p, double epsilon);
	}
}
