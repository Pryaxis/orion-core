using System.ComponentModel;

namespace Orion.World.Events
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.HardmodeStarting"/> event.
	/// </summary>
	public class HardmodeStartingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HardmodeStartingEventArgs"/> class.
		/// </summary>
		public HardmodeStartingEventArgs()
		{
		}
	}
}
