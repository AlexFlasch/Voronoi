using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
    public class VoronoiGraph : Entity
    {
        public class Vertex
        {
            public VoronoiPoint point;
            public List<VoronoiPoint> neighbors;
        }

        public class Edge
        {
            public Vertex node1;
            public Vertex node2;

            public double Length
            {
                get { return Util.GetDistance(node1.point.X, node1.point.Y, node2.point.X, node2.point.Y); }
            }
        }
    }
}
