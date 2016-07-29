using System.ComponentModel;

namespace Orion.World.Events
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.WorldSaving"/> event.
	/// </summary>
	public class WorldSavingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets a value indicating whether to reset the time.
		/// </summary>
		public bool ResetTime { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="WorldSavingEventArgs"/> class.
		/// </summary>
		/// <param name="resetTime">A value indicating whether to reset the time.</param>
		public WorldSavingEventArgs(bool resetTime)
		{
			ResetTime = resetTime;
		}
	}
}
