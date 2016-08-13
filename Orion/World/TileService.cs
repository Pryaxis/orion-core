using System;
using Orion.Framework;
using OTAPI;
using OTAPI.Tile;
using Terraria;

namespace Orion.World
{
	/// <summary>
	/// Implements the <see cref="ITileService"/> functionality. Mimics vanilla behaviour by storing tiles in a 2D tile
	/// array.
	/// </summary>
	[Service("Tile Service", Author = "Nyx Studios")]
	public class TileService : SharedService, ITileService
	{
		private ITile[,] _tiles;

		/// <summary>
		/// Gets or sets the <see cref="ITile"/> instance at the specified position.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <remarks>
		/// The 2D tile array is lazily constructed when getting <see cref="ITile"/> instances.
		/// </remarks>
		public ITile this[int x, int y]
		{
			get
			{
				if (_tiles == null)
				{
					_tiles = new ITile[Main.maxTilesX + 1, Main.maxTilesY + 1];
				}

				return _tiles[x, y];
			}
			set { _tiles[x, y] = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TileService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public TileService(Orion orion) : base(orion)
		{
			Hooks.Tile.CreateCollection = () => this;
		}
	}
}
