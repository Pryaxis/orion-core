using System.ComponentModel;

namespace Orion.World.Events
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.WorldLoading"/> event.
	/// </summary>
	public class WorldLoadingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WorldLoadingEventArgs"/> class.
		/// </summary>
		public WorldLoadingEventArgs()
		{
		}
	}
}
