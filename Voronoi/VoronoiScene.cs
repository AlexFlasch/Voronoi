using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
		private Surface voronoiSurface;
		private VoronoiGraph graph;
		private List<Entity> points;
		private List<Entity> cells;
		private Session player;

		public VoronoiScene() : base()
		{
			player = Session.Create("p1");

			voronoiSurface = new Surface(width, height, Color.Black);

			points = GeneratePoints(NumPoints);
			graph = GeneratePlanarGraph(points);
			/*AddSurface(voronoiSurface);*/
			Draw.SetTarget(voronoiSurface);

			Add(points);

			/*AddMultiple(GenerateCells().ToArray());*/
		}

		public override void Render()
		{
			base.Render();
			foreach (var edge in graph.GetEdges())
			{
				Draw.Line(edge.Node1.Point.X, edge.Node1.Point.Y, edge.Node2.Point.X, edge.Node2.Point.Y, Color.Cyan, 2);
			}
		}

		public override void Update()
		{
			base.Update();

			Button resetButton = new Button();
			resetButton.Keys.Add(Key.R);
			resetButton.Enabled = true;

			if (resetButton.Pressed || resetButton.Down)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Reset pressed!");
				Console.ForegroundColor = ConsoleColor.White;
				Surface.Clear();
				points = GeneratePoints(NumPoints);
				graph = GeneratePlanarGraph(points);

				Add(points);
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
						VoronoiGraph.Edge tempEdge = new VoronoiGraph.Edge(tempPoint, vertex.Point);
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
