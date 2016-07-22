using System.ComponentModel;
using Orion.Interfaces;

namespace Orion.Events.World
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.Saving"/> event.
	/// </summary>
	public class SavingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets a value indicating whether to reset the time.
		/// </summary>
		public bool ResetTime { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SavingEventArgs"/> class.
		/// </summary>
		/// <param name="resetTime">A value indicating whether to reset the time.</param>
		public SavingEventArgs(bool resetTime)
		{
			ResetTime = resetTime;
		}
	}
}
