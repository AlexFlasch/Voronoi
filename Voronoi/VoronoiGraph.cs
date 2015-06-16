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
            public VoronoiPoint Point;
            public List<VoronoiPoint> Neighbors;

	        public Vertex(VoronoiPoint point)
	        {
		        this.Point = point;
	        }

	        public void AddNeighbor(VoronoiPoint p)
	        {
		        Neighbors.Add(p);
	        }

	        public void RemoveNeighbor(VoronoiPoint p)
	        {
		        Neighbors.Remove(p);
	        }
        }
		//end Vertex class

        public class Edge
        {
            public Vertex Node1;
            public Vertex Node2;

            public double Length
            {
                get { return Util.GetDistance(Node1.Point.X, Node1.Point.Y, Node2.Point.X, Node2.Point.Y); }
            }

	        public Edge(VoronoiPoint a, VoronoiPoint b)
	        {
		        Node1 = new Vertex(a);
		        Node2 = new Vertex(b);
	        }

	        public bool Intersects(Edge e)
	        {
		        var line1 = new Line2(Node1.Point.X, Node1.Point.Y, Node2.Point.X, Node2.Point.Y);
				var line2 = new Line2(e.Node1.Point.X, e.Node1.Point.Y, e.Node2.Point.X, e.Node2.Point.Y);

		        return line2.Intersects(line1);
	        }
        }
		//end Edge class

	    private readonly List<Vertex> _vertices;
	    private readonly List<Edge> _edges;

	    public VoronoiGraph()
	    {
		    _vertices = new List<Vertex>();
			_edges = new List<Edge>();
	    }

	    public void AddVertex(VoronoiPoint point)
	    {
		    _vertices.Add(new Vertex(point));
	    }

	    public void AddEdge(VoronoiPoint a, VoronoiPoint b)
	    {
		    _edges.Add(new Edge(a, b));
	    }

	    public void AddEdge(Edge e)
	    {
		    _edges.Add(e);
	    }

	    public List<Vertex> GetVertices()
	    {
		    return _vertices;
	    }

	    public List<Edge> GetEdges()
	    {
		    return _edges;
	    } 
    }
}
