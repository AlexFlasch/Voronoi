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

		public List<List<Vertex>> DividedGraph { get; set; } 

        public class Vertex
        {
            public VoronoiPoint Point;
            public List<VoronoiPoint> Neighbors;

	        public Vertex(VoronoiPoint point)
	        {
		        Point = point;
	        }

	        public Vertex(double x, double y)
	        {
		        Point = new VoronoiPoint((float) x, (float) y);
	        }

	        public Vertex(int x, int y)
	        {
		        Point = new VoronoiPoint(x, y);
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

	    private readonly List<Vertex> _vertices;
	    private readonly List<Edge> _edges;

	    public VoronoiGraph()
	    {
		    _vertices = new List<Vertex>();
			_edges = new List<Edge>();

			DividedGraph = new List<List<Vertex>>();
	    }

	    public VoronoiGraph(List<VoronoiPoint> points)
	    {
			_vertices = new List<Vertex>();
			_edges = new List<Edge>();

			DividedGraph = new List<List<Vertex>>();

		    foreach (var point in points)
		    {
			    AddVertex(point);
		    }
	    }

		#region Private Methods

		#region Private Vertex Methods

	    private Vertex PointToVertex(VoronoiPoint p)
	    {
		    return new Vertex(p);
	    }

		#endregion

		#region Edge Methods

	    private Edge PointsToEdge(VoronoiPoint p1, VoronoiPoint p2)
	    {
		    return new Edge(p1, p2);
	    }

		#endregion

		#region Graph Methods

		private void OrderVertices()
		{
			_vertices.Sort((x1, x2) => x1.Point.X.CompareTo(x2.Point.X));

			//Add the order text to each VoronoiPoint
			/*for (int i = 0; i < _vertices.Count; i++)
			{
				string orderText = "" + i + "\nx: " + _vertices[i].Point.X + "\ny: " + _vertices[i].Point.Y;
				_vertices[i].Point.OrderText = new Text(orderText, 16);
			}*/
		}

	    private List<List<Vertex>> DivideGraph(List<Vertex> points)
		{
			List<Vertex> half1 = new List<Vertex>();
			List<Vertex> half2 = new List<Vertex>();

			for (int i = 0; i < points.Count; i++)
			{
				if (i < points.Count/2)
				{
					half1.Add(points.ElementAt(i));
				}
				else
				{
					half2.Add(points.ElementAt(i));
				}
			}

			var graphList = new List<List<Vertex>> {half1, half2};

		    return DivideGraphRec(graphList);
		}

	    private List<List<Vertex>> DivideGraphRec(List<List<Vertex>> graphList)
	    {
		    if (graphList.Count == 0)
		    {
			    return DividedGraph;
		    }

		    var workingList = graphList;

		    foreach (var list in graphList)
		    {
				if (list.Count <= 3)
				{
					//this list of points (graph) is reduced to where we need it.
					//add to the divided graph list to be merged.
					DividedGraph.Add(list);
					continue;
				}
			    if (list.Count == 1)
			    {
				    Console.ForegroundColor = ConsoleColor.Red;
				    Console.WriteLine("Uhhh... I did something wrong.");
				    Console.ForegroundColor = ConsoleColor.White;
				    throw new Exception("Yeah we got some problems.");
			    }
			    else
			    {
				    List<Vertex> half1 = new List<Vertex>();
				    List<Vertex> half2 = new List<Vertex>();

				    for (int i = 0; i < list.Count; i++)
				    {
					    if (i < list.Count/2)
					    {
						    half1.Add(list.ElementAt(i));
					    }
					    else
					    {
						    half2.Add(list.ElementAt(i));
					    }
				    }

				    workingList.Add(half1);
				    workingList.Add(half2);

				    DividedGraph.Clear();

					return DivideGraphRec(workingList);

			    }
		    }

		    return DividedGraph;
	    }

		#endregion

		#endregion

		#region Public Methods

		public void AddVertex(VoronoiPoint point)
	    {
		    _vertices.Add(new Vertex(point));
	    }

	    public void RemoveVertex(VoronoiPoint point)
	    {
		    Vertex v = PointToVertex(point);

		    _vertices.Remove(v);
		}

	    public void AddEdge(VoronoiPoint a, VoronoiPoint b)
	    {
		    _edges.Add(new Edge(a, b));
	    }

	    public void AddEdge(Edge e)
	    {
		    _edges.Add(e);
	    }

	    public void RemoveEdge(Edge e)
	    {
		    _edges.Remove(e);
	    }

	    public List<Vertex> GetVertices()
	    {
		    return _vertices;
	    }

	    public List<Edge> GetEdges()
	    {
		    return _edges;
		}

	    public void CreateDelaunayTriangulation()
	    {
			OrderVertices();
		    var dividedGraphs = DivideGraph(_vertices);

		    for (int i = 0; i < dividedGraphs.Count; i++)
		    {
			    List<Vertex> tempList = dividedGraphs.ElementAt(i);

			    for (int j = 0; j < tempList.Count; j++)
			    {
				    Console.ForegroundColor = ConsoleColor.Cyan;
				    var point = tempList.ElementAt(j).Point;
				    Console.WriteLine("Graph {0}: Point {1}: x: {2}, y{3}", i, j, point.X, point.Y);
			    }
		    }
	    }

		#endregion
	}
}
