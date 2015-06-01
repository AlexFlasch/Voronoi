using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
	class VoronoiPoint : Entity
	{
		private readonly Image pointImage = Image.CreateCircle(10, Color.White);

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
				if (Util.GetDistance(X, Y, point.X, point.Y) <= 50)
				{
					valid = false;
					break;
				}
				else if (X < 20 || X > DataSingleton.Instance.Width - 20
				         || Y < 20 || Y > DataSingleton.Instance.Height - 20)
				{
					valid = false;
					break;
				}
			}

			return valid;
		}
	}
}
