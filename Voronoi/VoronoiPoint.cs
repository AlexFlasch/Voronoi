using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
	public class VoronoiPoint : Entity
	{
		private readonly Image pointImage = Image.CreateCircle(10, Color.White);
	    private readonly int minDistance = 50;

		public VoronoiPoint(float x, float y) : base()
		{
			SetGraphic(pointImage);
			SetX(x);
			SetY(y);
		}

		public void SetX(float x)
		{
			X = x;
		}

		public void SetY(float y)
		{
			Y = y;
		}

		public bool IsValid(List<Entity> points)
		{
			bool valid = true;

			foreach (var point in points)
			{
				if (Util.GetDistance(X, Y, point.X, point.Y) <= minDistance)
				{
					valid = false;
					break;
				}
				else if (X < minDistance || X > DataSingleton.Instance.Width - minDistance
				         || Y < minDistance || Y > DataSingleton.Instance.Height - minDistance)
				{
					valid = false;
					break;
				}
			}

			return valid;
		}
	}
}
