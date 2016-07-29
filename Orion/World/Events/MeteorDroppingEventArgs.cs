using System.ComponentModel;

namespace Orion.World.Events
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.MeteorDropping"/> event.
	/// </summary>
	public class MeteorDroppingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets the x position that a meteor is dropping at.
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets the y position that a meteor is dropping at.
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MeteorDroppingEventArgs"/> class.
		/// </summary>
		/// <param name="x">The x position that a meteor is dropping at.</param>
		/// <param name="y">The y position that a meteor is dropping at.</param>
		public MeteorDroppingEventArgs(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
