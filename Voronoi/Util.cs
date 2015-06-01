using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi
{
	public static class Util
	{
		//static class for util methods.

		public static double GetDistance(float x1, float y1, float x2, float y2)
		{
			//use the distance formula distance = sqrt((x2-x1)^2 + (y2-y1)^2)
			return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
		}
	}
}
