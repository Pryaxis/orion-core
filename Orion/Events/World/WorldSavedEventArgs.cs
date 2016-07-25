using System;
using Orion.Interfaces;

namespace Orion.Events.World
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.WorldSaved"/> event.
	/// </summary>
	public class WorldSavedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WorldSavingEventArgs"/> class.
		/// </summary>
		public WorldSavedEventArgs()
		{
		}
	}
}
