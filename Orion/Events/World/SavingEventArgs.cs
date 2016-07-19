using Orion.Services;
using System.ComponentModel;

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
		/// Gets or sets a value indicating whether to save to the "cloud".
		/// </summary>
		public bool UseCloud { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SavingEventArgs"/> class with the specified options.
		/// </summary>
		/// <param name="useCloud">A value indicating whether to reset the time.</param>
		/// <param name="resetTime">A value indicating whether to save to the "cloud".</param>
		public SavingEventArgs(bool useCloud, bool resetTime)
		{
			UseCloud = useCloud;
			ResetTime = resetTime;
		}
	}
}
