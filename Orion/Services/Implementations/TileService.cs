using Orion.Framework;
using Orion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Orion.Services.Implementations
{
	/// <summary>
	/// Orion tile service.  Mimics vanilla behaviour by storing tiles in
	/// a 2D tile array.
	/// </summary>
	[Service(Author = "Nyx Studios", Name = "Tile Service")]
	public class TileService : ServiceBase, ITileService
	{
		protected Terraria.Tile[,] tileBuffer;

		public TileService(Orion orion) : base(orion)
		{
			OTAPI.Core.Hooks.Tile.CreateCollection = () => this;
		}

		public Tile this[int x, int y]
		{
			get
			{
				if (tileBuffer == null)
				{
					tileBuffer = new Tile[Main.maxTilesX + 1, Main.maxTilesY + 1];
				}

				return tileBuffer[x, y];
			}

			set
			{
				tileBuffer[x, y] = value;
			}
		}
	}
}
