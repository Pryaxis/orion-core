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
		/// Occurs when the game is checking if it is Christmas.
		/// </summary>
		event EventHandler<CheckingChristmasEventArgs> CheckingChristmas;

		/// <summary>
		/// Occurs when the game is checking if it is Halloween.
		/// </summary>
		event EventHandler<CheckingHalloweenEventArgs> CheckingHalloween;

		/// <summary>
		/// Occurs after hardmode has started.
		/// </summary>
		event EventHandler<HardmodeStartedEventArgs> HardmodeStarted;

		/// <summary>
		/// Occurs when hardmode is starting.
		/// </summary>
		event EventHandler<HardmodeStartingEventArgs> HardmodeStarting;

		/// <summary>
		/// Occurs when a tile is updating in hardmode.
		/// </summary>
		event EventHandler<HardmodeTileUpdatingEventArgs> HardmodeTileUpdating;

		/// <summary>
		/// Gets the world's height.
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently a blood moon.
		/// </summary>
		bool IsBloodMoon { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently Christmas.
		/// </summary>
		bool IsChristmas { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently daytime.
		/// </summary>
		bool IsDaytime { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently an eclipse.
		/// </summary>
		bool IsEclipse { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the world is in expert mode.
		/// </summary>
		bool IsExpertMode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently a frost moon.
		/// </summary>
		bool IsFrostMoon { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently Halloween.
		/// </summary>
		bool IsHalloween { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether it is currently a pumpkin moon.
		/// </summary>
		bool IsPumpkinMoon { get; set; }

		/// <summary>
		/// Occurs when a meteor is dropping.
		/// </summary>
		event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <summary>
		/// Gets or sets the time. This value is the number of ticks since the beginning of daytime or nighttime.
		/// </summary>
		double Time { get; set; }

		/// <summary>
		/// Gets the world's width.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Occurs after the world has loaded.
		/// </summary>
		event EventHandler<WorldLoadedEventArgs> WorldLoaded;

		/// <summary>
		/// Occurs when the world is loading.
		/// </summary>
		event EventHandler<WorldLoadingEventArgs> WorldLoading;

		/// <summary>
		/// Occurs after the world has saved.
		/// </summary>
		event EventHandler<WorldSavedEventArgs> WorldSaved;

		/// <summary>
		/// Occurs when the world is saving.
		/// </summary>
		event EventHandler<WorldSavingEventArgs> WorldSaving;

		/// <summary>
		/// Breaks the block at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is negative or greater than or equal to <see cref="Width"/>, or <paramref name="y"/>
		/// is negative or greater than or equal to<see cref="Height"/>.
		/// </exception>
		void BreakBlock(int x, int y);

		/// <summary>
		/// Breaks the wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is negative or greater than or equal to <see cref="Width"/>, or <paramref name="y"/>
		/// is negative or greater than or equal to<see cref="Height"/>.
		/// </exception>
		void BreakWall(int x, int y);

		/// <summary>
		/// Drops a meteor at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is less than 50 or greater than or equal to <see cref="Width"/> minus 50, or
		/// <paramref name="y"/> is less than 50 or greater than or equal to <see cref="Height"/> minus 50.
		/// </exception>
		void DropMeteor(int x, int y);

		/// <summary>
		/// Paints a block at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="color">The paint color.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is negative or greater than or equal to <see cref="Width"/>, or <paramref name="y"/>
		/// is negative or greater than or equal to<see cref="Height"/>.
		/// </exception>
		void PaintBlock(int x, int y, byte color);

		/// <summary>
		/// Paints a wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="color">The paint color.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is negative or greater than or equal to <see cref="Width"/>, or <paramref name="y"/>
		/// is negative or greater than or equal to<see cref="Height"/>.
		/// </exception>
		void PaintWall(int x, int y, byte color);

		/// <summary>
		/// Places a block at a position in the world, optionally with a style.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="block">The block type.</param>
		/// <param name="style">The style.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is negative or greater than or equal to <see cref="Width"/>, or <paramref name="y"/>
		/// is negative or greater than or equal to<see cref="Height"/>.
		/// </exception>
		void PlaceBlock(int x, int y, ushort block, int style = 0);

		/// <summary>
		/// Places a wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		/// <param name="wall">The wall type.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="x"/> is negative or greater than or equal to <see cref="Width"/>, or <paramref name="y"/>
		/// is negative or greater than or equal to<see cref="Height"/>.
		/// </exception>
		void PlaceWall(int x, int y, byte wall);

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
