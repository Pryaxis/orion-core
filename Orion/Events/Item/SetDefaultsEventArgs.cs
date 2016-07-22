using System;
using Orion.Interfaces;

namespace Orion.Events.Item
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.SetDefaults"/> event.
	/// </summary>
	public class SetDefaultsEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IItem"/> that had its defaults set.
		/// </summary>
		public IItem Item { get; }
		
		/// <summary>
		/// Gets the type ID that the <see cref="IItem"/> had its defaults set to.
		/// </summary>
		public int Type { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SetDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> that had its defaults set.</param>
		/// <param name="type">The type ID that the <see cref="IItem"/> had its defaults set to.</param>
		public SetDefaultsEventArgs(IItem item, int type)
		{
			Item = item;
			Type = type;
		}
	}
}
