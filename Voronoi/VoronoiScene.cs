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
		private List<VoronoiPoint> points;
		private List<Entity> cells;
		private Session player;

		public VoronoiScene() : base()
		{
			player = Session.Create("p1");

			voronoiSurface = new Surface(width, height, Color.Black);

			points = GeneratePoints(NumPoints);
			graph = new VoronoiGraph(points);
			graph.CreateDelaunayTriangulation();
			Draw.SetTarget(voronoiSurface);

			Add(points);
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

				Add(points);
			}
		}

		private List<VoronoiPoint> GeneratePoints(int numSites)
		{
			Random r = new Random();
			List<VoronoiPoint> points = new List<VoronoiPoint>();

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

		public override void Begin()
		{
			Console.WriteLine("And we're in!");
		}
	}
}
