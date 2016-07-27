using System;
using Orion.Events.World;
using Orion.Framework;

namespace Orion.Services
{
	/// <summary>
	/// Provides access to Terraria's world functions.
	/// </summary>
	public interface IWorldService : IService
	{
		/// <summary>
		/// Occurs when a meteor drops.
		/// </summary>
		event EventHandler<MeteorDroppingEventArgs> MeteorDropping;

		/// <summary>
		/// Occurs when a tile is updating in hardmode.
		/// </summary>
		event EventHandler<HardmodeTileUpdatingEventArgs> HardmodeTileUpdating;

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
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		void BreakBlock(int x, int y);

		/// <summary>
		/// Breaks the wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		void BreakWall(int x, int y);

		/// <summary>
		/// Places a block at a position in the world, optionally with a style.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		/// <param name="type">The tile type ID.</param>
		/// <param name="style">The style.</param>
		void PlaceBlock(int x, int y, ushort type, int style = 0);

		/// <summary>
		/// Places a wall at a position in the world.
		/// </summary>
		/// <param name="x">The x position in the world.</param>
		/// <param name="y">The y position in the world.</param>
		/// <param name="type">The wall type ID.</param>
		void PlaceWall(int x, int y, byte type);
	}
}
