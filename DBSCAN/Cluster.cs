using System.Collections.Generic;

namespace DBSCAN
{
	public class Cluster<T>
	{
		public IList<T> Objects { get; internal set; }
	}
}