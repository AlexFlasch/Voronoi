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
		private const int NumSites = 50;
		private List<Entity> points;
		private List<Entity> cells; 

		public VoronoiScene() : base()
		{
			AddMultiple(GeneratePoints(NumSites).ToArray());
			/*AddMultiple(GenerateCells().ToArray());*/
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
