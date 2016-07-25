using System;
using Orion.Events.World;
using Orion.Framework;
using Orion.Interfaces;
using OTAPI.Core;

namespace Orion.Services
{
	/// <summary>
	/// Manages Terraria's world functions.
	/// </summary>
	[Service("World Service", Author = "Nyx Studios")]
	public class WorldService : ServiceBase, IWorldService
	{
		/// <summary>
		/// A value indicating whether the service has been disposed. Used to ignore multiple
		/// <see cref="Dispose(bool)"/> calls.
		/// </summary>
		private bool _disposed;

		/// <summary>
		/// Occurs before a meteor drops.
		/// </summary>
		public event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <summary>
		/// Occurs before the world saves.
		/// </summary>
		public event EventHandler<SavedEventArgs> Saved;

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
			// TODO: add more events
			// TODO: add tests
			Hooks.World.DropMeteor = InvokeMeteorDropping;
			Hooks.World.IO.PreSaveWorld = InvokeSaving;
			Hooks.World.IO.PostSaveWorld = InvokeSaved;
		}

		/// <summary>
		/// Breaks the tile at a position in the world.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		public void BreakTile(int x, int y)
		{
			Terraria.WorldGen.KillTile(x, y);
		}

		/// <summary>
		/// Breaks the wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		public void BreakWall(int x, int y)
		{
			Terraria.WorldGen.KillWall(x, y);
		}

		/// <summary>
		/// Places a tile at a position in the world, optionally with a style.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		/// <param name="type">The tile type ID.</param>
		/// <param name="style">The style.</param>
		public void PlaceTile(int x, int y, ushort type, int style = 0)
		{
			Terraria.WorldGen.PlaceTile(x, y, type, style: style);
		}

		/// <summary>
		/// Places a wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		/// <param name="type">The wall type ID.</param>
		public void PlaceWall(int x, int y, byte type)
		{
			Terraria.WorldGen.PlaceWall(x, y, type);
		}

		/// <summary>
		/// Disposes the service and its unmanaged resources, if any, optionally disposing its managed resources, if
		/// any.
		/// </summary>
		/// <param name="disposing">
		/// true to dispose managed and unmanaged resources, false to only dispose unmanaged resources.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Hooks.World.DropMeteor = null;
					Hooks.World.IO.PreSaveWorld = null;
				}
				_disposed = true;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Invokes the <see cref="MeteorDropping"/> event.
		/// </summary>
		/// <param name="x">The x position in the world. This will update the normal server's value.</param>
		/// <param name="y">The y position in the world. This will update the normal server's value.</param>
		/// <returns></returns>
		private HookResult InvokeMeteorDropping(ref int x, ref int y)
		{
			var args = new MeteorDroppingEventArgs(x, y);
			MeteorDropping?.Invoke(this, args);
			x = args.X;
			y = args.Y;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		/// <summary>
		/// Invokes the <see cref="Saved"/> event.
		/// </summary>
		/// <param name="useCloud">A value indicating whether to use the "cloud". Unused.</param>
		/// <param name="resetTime">A value indicating whether to reset the time. Unused.</param>
		private void InvokeSaved(bool useCloud, bool resetTime)
		{
			var args = new SavedEventArgs();
			Saved?.Invoke(this, args);
		}

		/// <summary>
		/// Invokes the <see cref="Saving"/> event.
		/// </summary>
		/// <param name="useCloud">A value indicating whether to use the "cloud". Unused.</param>
		/// <param name="resetTime">
		/// A value indicating whether to reset the time. This will update hte normal server's value.
		/// </param>
		private HookResult InvokeSaving(ref bool useCloud, ref bool resetTime)
		{
			var args = new SavingEventArgs(resetTime);
			Saving?.Invoke(this, args);
			resetTime = args.ResetTime;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
