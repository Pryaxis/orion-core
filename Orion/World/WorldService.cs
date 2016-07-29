using System;
using Orion.Framework;
using Orion.World.Events;
using OTAPI.Core;

namespace Orion.World
{
	/// <summary>
	/// Manages Terraria's world functions.
	/// </summary>
	[Service("World Service", Author = "Nyx Studios")]
	public class WorldService : ServiceBase, IWorldService
	{
		private bool _disposed;

		/// <inheritdoc/>
		public event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <inheritdoc/>
		public event EventHandler<HardmodeTileUpdatingEventArgs> HardmodeTileUpdating;

		/// <inheritdoc/>
		public event EventHandler<WorldSavedEventArgs> WorldSaved;

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public void BreakBlock(int x, int y)
		{
			Terraria.WorldGen.KillTile(x, y);
		}

		/// <inheritdoc/>
		public void BreakWall(int x, int y)
		{
			Terraria.WorldGen.KillWall(x, y);
		}

		/// <inheritdoc/>
		public void PlaceBlock(int x, int y, ushort type, int style = 0)
		{
			Terraria.WorldGen.PlaceTile(x, y, type, style: style);
		}

		/// <inheritdoc/>
		public void PlaceWall(int x, int y, byte type)
		{
			Terraria.WorldGen.PlaceWall(x, y, type);
		}

		/// <inheritdoc/>
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

		private HookResult InvokeMeteorDropping(ref int x, ref int y)
		{
			var args = new MeteorDroppingEventArgs(x, y);
			MeteorDropping?.Invoke(this, args);
			x = args.X;
			y = args.Y;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private HardmodeTileUpdateResult InvokeHardmodeTileUpdating(int x, int y, ref ushort type)
		{
			var args = new HardmodeTileUpdatingEventArgs(x, y, type);
			HardmodeTileUpdating?.Invoke(this, args);
			type = args.Type;
			return args.Handled ? HardmodeTileUpdateResult.ContinueWithoutUpdate : HardmodeTileUpdateResult.Continue;
		}

		private void InvokeWorldSaved(bool useCloud, bool resetTime)
		{
			var args = new WorldSavedEventArgs(resetTime);
			WorldSaved?.Invoke(this, args);
		}

		private HookResult InvokeWorldSaving(ref bool useCloud, ref bool resetTime)
		{
			var args = new WorldSavingEventArgs(resetTime);
			WorldSaving?.Invoke(this, args);
			resetTime = args.ResetTime;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
