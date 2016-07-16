using Orion.Framework;
using Orion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Orion.Services
{
	public class TileService : ServiceBase, ITileService
	{
		public TileService(Orion orion) : base(orion)
		{
		}

		public Tile this[int x, int y]
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
