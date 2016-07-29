using System.ComponentModel;

namespace Orion.World.Events
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.HardmodeTileUpdating"/> event.
	/// </summary>
	public class HardmodeTileUpdatingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets the type ID that the tile is being updated to.
		/// </summary>
		public ushort Type { get; set; }

		/// <summary>
		/// Gets the x position of the tile that is updating.
		/// </summary>
		public int X { get; }

		/// <summary>
		/// Gets the y position of the tile that is updating.
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HardmodeTileUpdatingEventArgs"/> class.
		/// </summary>
		/// <param name="x">The x position of the tile that is updating.</param>
		/// <param name="y">The y position of the tile that is updating.</param>
		/// <param name="type">The type ID that the tile is being updated to.</param>
		public HardmodeTileUpdatingEventArgs(int x, int y, ushort type)
		{
			X = x;
			Y = y;
			Type = type;
		}
	}
}
