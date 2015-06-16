using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
	class LineEntity : Entity
	{
		private float x1, y1, x2, y2;

		public LineEntity(float x1, float y1, float x2, float y2) : base()
		{
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
		}
	}
}
