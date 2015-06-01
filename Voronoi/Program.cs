using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Voronoi
{
	class Program
	{
		static void Main(string[] args)
		{
			Game game = new Game("Game", 2560, 1440); //creates a game with internal resolution 3840x2160
			game.SetWindow(2560, 1440); ; //outputs the game to a window scaled down to 2560x1440
			game.MouseVisible = true;
			DataSingleton.Instance.Width = 2560;
			DataSingleton.Instance.Height = 1440;

			game.FirstScene = new VoronoiScene();

			game.Start(); //starts the game loop
		}
	}
}
