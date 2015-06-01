using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi
{
	public class DataSingleton
	{
		private static volatile DataSingleton instance; //There should only ever be one instance of this class (hence Signleton)
		private static object syncRoot = new Object();

		private DataSingleton() { }

		public static DataSingleton Instance
		{
			get
			{
				if (instance != null) return instance;
				lock (syncRoot)
				{
					if (instance == null)
					{
						instance = new DataSingleton();
					}
				}

				return instance;
			}
		}

		public int Width { get; set; }
		public int Height { get; set; }


	}
}
