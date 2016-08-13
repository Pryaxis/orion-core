using System;
using Orion.Framework;
using Orion.World.Events;
using OTAPI;

namespace Orion.World
{
	/// <summary>
	/// Manages Terraria's world functions.
	/// </summary>
	[Service("World Service", Author = "Nyx Studios")]
	public class WorldService : SharedService, IWorldService
	{
		/// <inheritdoc/>
		public event EventHandler<CheckingChristmasEventArgs> CheckingChristmas;

		/// <inheritdoc/>
		public event EventHandler<CheckingHalloweenEventArgs> CheckingHalloween;

		/// <inheritdoc/>
		public event EventHandler<HardmodeStartedEventArgs> HardmodeStarted;

		/// <inheritdoc/>
		public event EventHandler<HardmodeStartingEventArgs> HardmodeStarting;

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
		public event EventHandler<WorldLoadedEventArgs> WorldLoaded;

		/// <inheritdoc/>
		public event EventHandler<WorldLoadingEventArgs> WorldLoading;

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
			// TODO: add initialize, update
			Hooks.Game.Christmas = InvokeCheckingChristmas;
			Hooks.Game.Halloween = InvokeCheckingHalloween;
			Hooks.World.DropMeteor = InvokeMeteorDropping;
			Hooks.World.PreHardmode = InvokeHardmodeStarting;
			Hooks.World.PostHardmode = InvokeHardmodeStarted;
			Hooks.World.HardmodeTileUpdate = InvokeHardmodeTileUpdating;
			Hooks.World.IO.PreLoadWorld = InvokeWorldLoading;
			Hooks.World.IO.PostLoadWorld = InvokeWorldLoaded;
			Hooks.World.IO.PreSaveWorld = InvokeWorldSaving;
			Hooks.World.IO.PostSaveWorld = InvokeWorldSaved;
		}
		
		/// <inheritdoc/>
		public void BreakBlock(int x, int y)
		{
			if (x < 0 || x >= Width)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be non-negative and less than the world's width.");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be non-negative and less than the world's height.");
			}

			Terraria.WorldGen.KillTile(x, y);
		}

		/// <inheritdoc/>
		public void BreakWall(int x, int y)
		{
			if (x < 0 || x >= Width)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be non-negative and less than the world's width.");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be non-negative and less than the world's height.");
			}

			Terraria.WorldGen.KillWall(x, y);
		}

		/// <inheritdoc/>
		public void DropMeteor(int x, int y)
		{
			if (x < 50 || x >= Width - 50)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be at least 50 and less than the world's width minus 50.");
			}
			if (y < 50 || y >= Height - 50)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be at least 50 and less than the world's height minus 50.");
			}

			Terraria.WorldGen.meteor(x, y);
		}

		/// <inheritdoc/>
		public void PaintBlock(int x, int y, byte color)
		{
			if (x < 0 || x >= Width)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be non-negative and less than the world's width.");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be non-negative and less than the world's height.");
			}

			Terraria.WorldGen.paintTile(x, y, color);
		}

		/// <inheritdoc/>
		public void PaintWall(int x, int y, byte color)
		{
			if (x < 0 || x >= Width)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be non-negative and less than the world's width.");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be non-negative and less than the world's height.");
			}

			Terraria.WorldGen.paintWall(x, y, color);
		}

		/// <inheritdoc/>
		public void PlaceBlock(int x, int y, ushort block, int style = 0)
		{
			if (x < 0 || x >= Width)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be non-negative and less than the world's width.");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be non-negative and less than the world's height.");
			}

			Terraria.WorldGen.PlaceTile(x, y, block, style: style);
		}

		/// <inheritdoc/>
		public void PlaceWall(int x, int y, byte wall)
		{
			if (x < 0 || x >= Width)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"X position was out of range. Must be non-negative and less than the world's width.");
			}
			if (y < 0 || y >= Height)
			{
				throw new ArgumentOutOfRangeException(nameof(x),
					"Y position was out of range. Must be non-negative and less than the world's height.");
			}

			Terraria.WorldGen.PlaceWall(x, y, wall);
		}

		/// <inheritdoc/>
		public void Save(bool resetTime = false) => Terraria.IO.WorldFile.saveWorld(false, resetTime);

		/// <inheritdoc/>
		public void SettleLiquids() => Terraria.Liquid.StartPanic();

		private HookResult InvokeCheckingChristmas()
		{
			var args = new CheckingChristmasEventArgs();
			CheckingChristmas?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private HookResult InvokeCheckingHalloween()
		{
			var args = new CheckingHalloweenEventArgs();
			CheckingHalloween?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private void InvokeHardmodeStarted()
		{
			var args = new HardmodeStartedEventArgs();
			HardmodeStarted?.Invoke(this, args);
		}

		private HookResult InvokeHardmodeStarting()
		{
			var args = new HardmodeStartingEventArgs();
			HardmodeStarting?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
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

		private void InvokeWorldLoaded(bool useCloud)
		{
			var args = new WorldLoadedEventArgs();
			WorldLoaded?.Invoke(this, args);
		}

		private HookResult InvokeWorldLoading(ref bool useCloud)
		{
			var args = new WorldLoadingEventArgs();
			WorldLoading?.Invoke(this, args);
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
