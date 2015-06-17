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
		private readonly static int radius = 5;
		private readonly Image pointImage = Image.CreateCircle(radius, Color.White);
	    private readonly int minDistance = 50;
		private Text _orderText;

		public Text OrderText
		{
			get { return _orderText; }
			set
			{
				_orderText = value;
				AddGraphic(_orderText, pointImage.X, pointImage.Y - 75);
			}
		}

		public VoronoiPoint(float x, float y) : base()
		{
			pointImage.X -= radius;
			pointImage.Y -= radius;
			Image.CirclePointCount = 100;
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

		public bool IsValid(List<VoronoiPoint> points)
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
