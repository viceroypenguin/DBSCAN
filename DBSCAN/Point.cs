using System;
using System.Collections.Generic;
using System.Text;

namespace DBSCAN
{
    public struct Point : IPointData
    {
		public double X;
		public double Y;

		Point IPointData.Point => this;
    }
}
