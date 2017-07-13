using System;
using System.Collections.Generic;
using System.Text;

namespace DBSCAN
{
	public interface ISpatialIndex<T>
	{
		IList<T> Search();
		IList<T> Search(Point p, double epsilon);
	}
}
