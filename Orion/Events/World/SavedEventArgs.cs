using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Interfaces;

namespace Orion.Events.World
{
	/// <summary>
	/// Provides data for the <see cref="IWorldService.Saved"/> event.
	/// </summary>
	public class SavedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SavingEventArgs"/> class.
		/// </summary>
		public SavedEventArgs()
		{
		}
	}
}
