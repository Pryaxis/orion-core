using System;
using Orion.Entities.Item;

namespace Orion.Events.Item
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.ItemSetDefaults"/> event.
	/// </summary>
	public class ItemSetDefaultsEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IItem"/> that had its defaults set.
		/// </summary>
		public IItem Item { get; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ItemSetDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> that had its defaults set.</param>
		/// <exception cref="ArgumentNullException"><paramref name="item"/> was null.</exception>
		public ItemSetDefaultsEventArgs(IItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			Item = item;
		}
	}
}
