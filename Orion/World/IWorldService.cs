using System;
using Orion.Framework;
using Orion.World.Events;

namespace Orion.World
{
	/// <summary>
	/// Provides access to Terraria's world functions.
	/// </summary>
	public interface IWorldService : IService
	{
		/// <summary>
		/// Occurs when a tile is updating in hardmode.
		/// </summary>
		event EventHandler<HardmodeTileUpdatingEventArgs> HardmodeTileUpdating;

		/// <summary>
		/// Gets or sets a value indicating whether there is currently a blood moon.
		/// </summary>
		bool IsBloodMoon { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently daytime.
		/// </summary>
		bool IsDaytime { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether there is currently an eclipse.
		/// </summary>
		bool IsEclipse { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the world is in expert mode.
		/// </summary>
		bool IsExpertMode { get; set; }

		/// <summary>
		/// Occurs when a meteor drops.
		/// </summary>
		event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <summary>
		/// Gets or sets the time. This value is the number of ticks since daytime/nighttime.
		/// </summary>
		double Time { get; set; }

		/// <summary>
		/// Occurs after the world saved.
		/// </summary>
		event EventHandler<WorldSavedEventArgs> WorldSaved;

		/// <summary>
		/// Occurs when the world saves.
		/// </summary>
		event EventHandler<WorldSavingEventArgs> WorldSaving;

		/// <summary>
		/// Breaks the block at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		void BreakBlock(int x, int y);

		/// <summary>
		/// Breaks the wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		void BreakWall(int x, int y);

		/// <summary>
		/// Drops a meteor at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		void DropMeteor(int x, int y);

		/// <summary>
		/// Places a block at a position in the world, optionally with a style.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="type">The tile type ID.</param>
		/// <param name="style">The style.</param>
		void PlaceBlock(int x, int y, ushort type, int style = 0);

		/// <summary>
		/// Places a wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="type">The wall type ID.</param>
		void PlaceWall(int x, int y, byte type);

		/// <summary>
		/// Saves the world.
		/// </summary>
		/// <param name="resetTime">A value indicating whether to reset the time.</param>
		void Save(bool resetTime = false);

		/// <summary>
		/// Settles liquids, forcing them to update.
		/// </summary>
		void SettleLiquids();
	}
}
