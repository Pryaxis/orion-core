using System;
using System.ComponentModel;
using Orion.Interfaces;

namespace Orion.Events.Item
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.ItemSettingDefaults"/> event.
	/// </summary>
	public class ItemSettingDefaultsEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="IItem"/> that is having its defaults set.
		/// </summary>
		public IItem Item { get; }

		/// <summary>
		/// Gets or sets the type ID that the <see cref="IItem"/> is having its defaults set to.
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemSettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> that is having its defaults set.</param>
		/// <param name="type">The type ID that the <see cref="IItem"/> is having its defaults set to.</param>
		/// <exception cref="ArgumentNullException"><paramref name="item"/> was null.</exception>
		public ItemSettingDefaultsEventArgs(IItem item, int type)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			Item = item;
			Type = type;
		}
	}
}
