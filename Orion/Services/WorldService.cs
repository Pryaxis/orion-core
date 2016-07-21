using System;
using Orion.Events.World;
using Orion.Framework;
using Orion.Interfaces;
using OTAPI.Core;

namespace Orion.Services
{
	/// <summary>
	/// Implements the <see cref="IWorldService"/> functionality.
	/// </summary>
	[Service("World Service", Author = "Nyx Studios")]
	public class WorldService : ServiceBase, IWorldService
	{
		/// <summary>
		/// Occurs before a meteor drops.
		/// </summary>
		public event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <summary>
		/// Occurs before the world saves.
		/// </summary>
		public event EventHandler<SavingEventArgs> Saving;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorldService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public WorldService(Orion orion) : base(orion)
		{
			Hooks.World.DropMeteor = InvokeMeteorDropping;
			Hooks.World.IO.PreSaveWorld = InvokeSaving;
		}

		/// <summary>
		/// Breaks the tile at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		public void BreakTile(int x, int y)
		{
			Terraria.WorldGen.KillTile(x, y);
		}

		/// <summary>
		/// Breaks the wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		public void BreakWall(int x, int y)
		{
			Terraria.WorldGen.KillWall(x, y);
		}

		/// <summary>
		/// Places a tile at a position in the world, optionally with a style.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="type">The tile type ID.</param>
		/// <param name="style">The style.</param>
		public void PlaceTile(int x, int y, ushort type, int style = 0)
		{
			Terraria.WorldGen.PlaceTile(x, y, type, style: style);
		}

		/// <summary>
		/// Places a wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="type">The wall type ID.</param>
		public void PlaceWall(int x, int y, byte type)
		{
			Terraria.WorldGen.PlaceWall(x, y, type);
		}

		private HookResult InvokeMeteorDropping(ref int x, ref int y)
		{
			var args = new MeteorDroppingEventArgs(x, y);
			MeteorDropping?.Invoke(this, args);
			x = args.X;
			y = args.Y;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private HookResult InvokeSaving(ref bool useCloud, ref bool resetTime)
		{
			var args = new SavingEventArgs(useCloud, resetTime);
			Saving?.Invoke(this, args);
			useCloud = args.UseCloud;
			resetTime = args.ResetTime;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
