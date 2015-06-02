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

	        public Vertex(VoronoiPoint point)
	        {
		        this.point = point;
	        }
        }
		//end Vertex class

        public class Edge
        {
            public Vertex node1;
            public Vertex node2;

            public double Length
            {
                get { return Util.GetDistance(node1.point.X, node1.point.Y, node2.point.X, node2.point.Y); }
            }

	        public Edge(VoronoiPoint a, VoronoiPoint b)
	        {
		        node1 = new Vertex(a);
		        node2 = new Vertex(b);
	        }

	        public bool Intersects(Edge e)
	        {
		        return DoBoundingBoxesIntersect(this, e)
		               && LineSegmentTouchesOrCrossesLine(this, e)
		               && LineSegmentTouchesOrCrossesLine(e, this);
	        }

	        private bool DoBoundingBoxesIntersect(Edge a, Edge b)
	        {
		        return a.node1.point.X <= b.node2.point.X
		               && a.node2.point.X >= b.node1.point.X
		               && a.node1.point.Y <= b.node2.point.Y
		               && a.node2.point.Y >= b.node1.point.Y;
	        }

	        private bool isPointOnLine(Edge a, Vertex b)
	        {
		        var aTemp = new Edge(new VoronoiPoint(0, 0),
					new VoronoiPoint(a.node2.point.X - a.node1.point.X, a.node2.point.Y - a.node1.point.Y) );
		        var bTemp = new Vertex(new VoronoiPoint(b.point.X - a.node1.point.X, b.point.Y - a.node1.point.Y));

		        var r = Util.CrossProduct2D(aTemp.node2, bTemp);

		        return Math.Abs(r) < Double.Epsilon;
	        }

	        private bool isPointRightOfLine(Edge a, Vertex b)
	        {
		        var aTemp = new Edge(new VoronoiPoint(0, 0),
			        new VoronoiPoint(a.node2.point.X - a.node1.point.X, a.node2.point.Y - a.node1.point.Y));
				var bTemp = new Vertex(new VoronoiPoint(b.point.X - a.node1.point.X, b.point.Y - a.node1.point.Y));

		        return Util.CrossProduct2D(aTemp.node2, bTemp) < 0;
	        }

	        private bool LineSegmentTouchesOrCrossesLine(Edge a, Edge b)
	        {
		        return isPointOnLine(a, b.node1)
		               || isPointOnLine(a, b.node2)
		               || (isPointRightOfLine(a, b.node1) ^
		                   isPointRightOfLine(a, b.node2));
	        }
        }
		//end Edge class

	    private List<Vertex> vertices;
	    private List<Edge> edges;

	    public VoronoiGraph()
	    {
		    vertices = new List<Vertex>();
			edges = new List<Edge>();
	    }

	    public void AddVertex(VoronoiPoint point)
	    {
		    vertices.Add(new Vertex(point));
	    }

	    public void AddEdge(VoronoiPoint a, VoronoiPoint b)
	    {
		    edges.Add(new Edge(a, b));
	    }

	    public void AddEdge(Edge e)
	    {
		    edges.Add(e);
	    }

	    public List<Vertex> GetVertices()
	    {
		    return vertices;
	    }

	    public List<Edge> GetEdges()
	    {
		    return edges;
	    } 
    }
}
