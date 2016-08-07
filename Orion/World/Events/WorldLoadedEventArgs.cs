using System;

namespace Orion.World.Events
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.WorldLoaded"/> event.
	/// </summary>
	public class WorldLoadedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WorldLoadedEventArgs"/> class.
		/// </summary>
		public WorldLoadedEventArgs()
		{
		}
	}
}
