using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
	class VoronoiCell : Entity
	{
		private VoronoiPoint point;
		public VoronoiCell Left { get; set; }
		public VoronoiCell Right { get; set; }
		public VoronoiCell Up { get; set; }
		public VoronoiCell Down { get; set; }

		public VoronoiCell(VoronoiPoint point)
		{
			this.point = point;
		}
	}
}
