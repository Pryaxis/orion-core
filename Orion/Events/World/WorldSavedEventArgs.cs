using System;
using Orion.Services;

namespace Orion.Events.World
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.WorldSaved"/> event.
	/// </summary>
	public class WorldSavedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets a value indicating whether to reset the time.
		/// </summary>
		public bool ResetTime { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WorldSavedEventArgs"/> class.
		/// </summary>
		/// <param name="resetTime">A value indicating whether to reset the time.</param>
		public WorldSavedEventArgs(bool resetTime)
		{
			ResetTime = resetTime;
		}
	}
}
