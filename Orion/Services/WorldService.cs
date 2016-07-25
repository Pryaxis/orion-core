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
		/// Occurs when a meteor drops.
		/// </summary>
		public event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <summary>
		/// Occurs when a tile is updating in hardmode.
		/// </summary>
		public event EventHandler<HardmodeTileUpdatingEventArgs> HardmodeTileUpdating;

		/// <summary>
		/// Occurs after the world has saved.
		/// </summary>
		public event EventHandler<WorldSavedEventArgs> WorldSaved;

		/// <summary>
		/// Occurs when the world saves.
		/// </summary>
		public event EventHandler<WorldSavingEventArgs> WorldSaving;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorldService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public WorldService(Orion orion) : base(orion)
		{
			// TODO: add christmas, halloween, initialize, update
			Hooks.World.DropMeteor = InvokeMeteorDropping;
			Hooks.World.HardmodeTileUpdate = InvokeHardmodeTileUpdating;
			Hooks.World.IO.PreSaveWorld = InvokeWorldSaving;
			Hooks.World.IO.PostSaveWorld = InvokeWorldSaved;
		}

		/// <summary>
		/// Breaks the block at a position in the world.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		public void BreakBlock(int x, int y)
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
		/// Places a block at a position in the world, optionally with a style.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		/// <param name="type">The tile type ID.</param>
		/// <param name="style">The style.</param>
		public void PlaceBlock(int x, int y, ushort type, int style = 0)
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
		/// Disposes the service and its unmanaged resources, optionally disposing its managed resources.
		/// </summary>
		/// <param name="disposing">
		/// true if called from a managed disposal, and *both* unmanaged and managed resources must be freed. false
		/// if called from a finalizer, and *only* unmanaged resources may be freed.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Hooks.World.DropMeteor = null;
					Hooks.World.HardmodeTileUpdate = null;
					Hooks.World.IO.PreSaveWorld = null;
					Hooks.World.IO.PostSaveWorld = null;
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
		/// <returns>A value indicating whether to continue or cancel normal server code.</returns>
		private HookResult InvokeMeteorDropping(ref int x, ref int y)
		{
			var args = new MeteorDroppingEventArgs(x, y);
			MeteorDropping?.Invoke(this, args);
			x = args.X;
			y = args.Y;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		/// <summary>
		/// Invokes the <see cref="HardmodeTileUpdating"/> event.
		/// </summary>
		/// <param name="x">The x position of the tile that is updating.</param>
		/// <param name="y">The y position of the tile that is updating.</param>
		/// <param name="type">
		/// The type ID of the tile that is updating. This will update the normal server's value.
		/// </param>
		/// <returns>A value indicating whether to continue or cancel normal server code.</returns>
		private HardmodeTileUpdateResult InvokeHardmodeTileUpdating(int x, int y, ref ushort type)
		{
			var args = new HardmodeTileUpdatingEventArgs(x, y, type);
			HardmodeTileUpdating?.Invoke(this, args);
			type = args.Type;
			return args.Handled ? HardmodeTileUpdateResult.ContinueWithoutUpdate : HardmodeTileUpdateResult.Continue;
		}

		/// <summary>
		/// Invokes the <see cref="WorldSaved"/> event.
		/// </summary>
		/// <param name="useCloud">A value indicating whether to use the "cloud". Unused.</param>
		/// <param name="resetTime">A value indicating whether to reset the time.</param>
		private void InvokeWorldSaved(bool useCloud, bool resetTime)
		{
			var args = new WorldSavedEventArgs(resetTime);
			WorldSaved?.Invoke(this, args);
		}

		/// <summary>
		/// Invokes the <see cref="WorldSaving"/> event.
		/// </summary>
		/// <param name="useCloud">A value indicating whether to use the "cloud". Unused.</param>
		/// <param name="resetTime">
		/// A value indicating whether to reset the time. This will update the normal server's value.
		/// </param>
		/// <returns>A value indicating whether to continue or cancel normal server code.</returns>
		private HookResult InvokeWorldSaving(ref bool useCloud, ref bool resetTime)
		{
			var args = new WorldSavingEventArgs(resetTime);
			WorldSaving?.Invoke(this, args);
			resetTime = args.ResetTime;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
