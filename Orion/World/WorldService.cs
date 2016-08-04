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
		public event EventHandler<HardmodeTileUpdatingEventArgs> HardmodeTileUpdating;

		/// <inheritdoc/>
		public int Height => Terraria.Main.maxTilesY;

		/// <inheritdoc/>
		public bool IsBloodMoon
		{
			get { return Terraria.Main.bloodMoon; }
			set { Terraria.Main.bloodMoon = value; }
		}

		/// <inheritdoc/>
		public bool IsChristmas
		{
			get { return Terraria.Main.xMas; }
			set { Terraria.Main.xMas = value; }
		}

		/// <inheritdoc/>
		public bool IsDaytime
		{
			get { return Terraria.Main.dayTime; }
			set { Terraria.Main.dayTime = value; }
		}

		/// <inheritdoc/>
		public bool IsEclipse
		{
			get { return Terraria.Main.eclipse; }
			set { Terraria.Main.eclipse = value; }
		}

		/// <inheritdoc/>
		public bool IsExpertMode
		{
			get { return Terraria.Main.expertMode; }
			set { Terraria.Main.expertMode = value; }
		}

		/// <inheritdoc/>
		public bool IsFrostMoon
		{
			get { return Terraria.Main.snowMoon; }
			set { Terraria.Main.snowMoon = value; }
		}

		/// <inheritdoc/>
		public bool IsHalloween
		{
			get { return Terraria.Main.halloween; }
			set { Terraria.Main.halloween = value; }
		}

		/// <inheritdoc/>
		public bool IsPumpkinMoon
		{
			get { return Terraria.Main.pumpkinMoon; }
			set { Terraria.Main.pumpkinMoon = value; }
		}

		/// <inheritdoc/>
		public event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <inheritdoc/>
		public double Time
		{
			get { return Terraria.Main.time; }
			set { Terraria.Main.time = value; }
		}

		/// <inheritdoc/>
		public int Width => Terraria.Main.maxTilesX;

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
		public void BreakBlock(int x, int y) => Terraria.WorldGen.KillTile(x, y);

		/// <inheritdoc/>
		public void BreakWall(int x, int y) => Terraria.WorldGen.KillWall(x, y);

		/// <inheritdoc/>
		public void DropMeteor(int x, int y) => Terraria.WorldGen.meteor(x, y);

		/// <inheritdoc/>
		public void PlaceBlock(int x, int y, ushort type, int style = 0) =>
			Terraria.WorldGen.PlaceTile(x, y, type, style: style);

		/// <inheritdoc/>
		public void PlaceWall(int x, int y, byte type) => Terraria.WorldGen.PlaceWall(x, y, type);

		/// <inheritdoc/>
		public void Save(bool resetTime = false) => Terraria.IO.WorldFile.saveWorld(false, resetTime);

		/// <inheritdoc/>
		public void SettleLiquids() => Terraria.Liquid.StartPanic();

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

		private HardmodeTileUpdateResult InvokeHardmodeTileUpdating(int x, int y, ref ushort type)
		{
			var args = new HardmodeTileUpdatingEventArgs(x, y, type);
			HardmodeTileUpdating?.Invoke(this, args);
			type = args.Type;
			return args.Handled ? HardmodeTileUpdateResult.ContinueWithoutUpdate : HardmodeTileUpdateResult.Continue;
		}

		private HookResult InvokeMeteorDropping(ref int x, ref int y)
		{
			var args = new MeteorDroppingEventArgs(x, y);
			MeteorDropping?.Invoke(this, args);
			x = args.X;
			y = args.Y;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
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
