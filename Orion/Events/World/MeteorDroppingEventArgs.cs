using Orion.Services;
using System.ComponentModel;

namespace Orion.Events.World
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.MeteorDropping"/> event.
	/// </summary>
	public class MeteorDroppingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets the x position.
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets the y position.
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MeteorDroppingEventArgs"/> class with the specified positions.
		/// </summary>
		/// <param name="x">The x position.</param>
		/// <param name="y">The y position.</param>
		public MeteorDroppingEventArgs(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
