using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
	class VoronoiScene : Scene
	{
		private int width;
		private int height;
		private const int NumPoints = 50;
		private VoronoiGraph graph;
		private List<Entity> points;
		private List<Entity> cells; 

		public VoronoiScene() : base()
		{
			Surface voronoiSurface = new Surface(width, height, Color.Black);

			points = GeneratePoints(NumPoints);
			graph = GeneratePlanarGraph(points);
			Draw.SetTarget(voronoiSurface);

			AddMultiple(points.ToArray());
			AddSurface(voronoiSurface);
			/*AddMultiple(GenerateCells().ToArray());*/

			Render();
		}

		public override void Render()
		{
			base.Render();
			foreach (var edge in graph.GetEdges())
			{
				Draw.Line(edge.node1.point.X, edge.node1.point.Y, edge.node2.point.X, edge.node2.point.Y, Color.Cyan, 2);
			}
		}

		private List<Entity> GeneratePoints(int numSites)
		{
			Random r = new Random();
			List<Entity> points = new List<Entity>();

			int i = 0;
			while(i < numSites)
			{
				float randX = (float) r.NextDouble() * DataSingleton.Instance.Width;
				float randY = (float) r.NextDouble() * DataSingleton.Instance.Height;
				VoronoiPoint point = new VoronoiPoint(randX, randY);
				if (!point.IsValid(points))
				{
					continue;
				}
				Console.WriteLine("Putting point at x:{0} y:{1}", randX, randY);
				points.Add(new VoronoiPoint(randX, randY));
				i++;
			}

			this.points = points;
			return points;
		}

		private VoronoiGraph GeneratePlanarGraph(List<Entity> points)
		{
			var graph = new VoronoiGraph();

			foreach (var point in points)
			{
				var tempPoint = new VoronoiPoint(point.X, point.Y);
				graph.AddVertex(tempPoint);
				if (graph.GetVertices().Count > 1)
				{
					/* "Shorthand"...?
					 * foreach (VoronoiGraph.Edge tempEdge 
						in from vertex in graph.GetVertices() 
							select new VoronoiGraph.Edge(tempPoint, vertex.point)
							into tempEdge
							where graph.GetEdges().Count > 1 
								let validEdge = graph.GetEdges().All(edge => !edge.Intersects(tempEdge))
									where validEdge
										select tempEdge)
					{
						graph.AddEdge(tempEdge);
					}
					 */
					foreach (var vertex in graph.GetVertices())
					{
						VoronoiGraph.Edge tempEdge = new VoronoiGraph.Edge(tempPoint, vertex.point);
						if (graph.GetVertices().Count <= 1) continue;
						bool validEdge = graph.GetEdges().All(edge => !edge.Intersects(tempEdge));

						if (validEdge)
						{
							graph.AddEdge(tempEdge);
						}
					}
				}
			}

			return graph;
		}

		/*private List<Entity> GenerateCells()
		{
			//make a new cell for each point generated
			foreach (var point in points)
			{
				cells.Add(new VoronoiCell((VoronoiPoint)point));
			}
			//start with the first cell, find the nearest cell to it
			foreach (var cell in cells)
			{
				var otherCells = cells.Where(c => c != cell).ToList();
				Entity closestCell = null;
				foreach (var otherCell in otherCells)
				{
					if (closestCell == null
					    ||
					    Util.GetDistance(otherCell.X, otherCell.Y, cell.X, cell.Y) <
					    Util.GetDistance(closestCell.X, closestCell.Y, cell.X, cell.Y))
					{
						closestCell = otherCell;
					}
				}

			}
		}*/

		public override void Begin()
		{
			Console.WriteLine("And we're in!");
		}
	}
}
