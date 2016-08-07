using System;
using System.ComponentModel;

namespace Orion.Items.Events
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.ItemSettingDefaults"/> event.
	/// </summary>
	public class ItemSettingDefaultsEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="IItem"/> instance that is having its defaults set.
		/// </summary>
		public IItem Item { get; }

		/// <summary>
		/// Gets or sets the <see cref="ItemType"/> that the <see cref="IItem"/> instance is having its defaults set to.
		/// </summary>
		public ItemType Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemSettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> instance that is having its defaults set.</param>
		/// <param name="type">
		/// The <see cref="ItemType"/> that the <see cref="IItem"/> instance is having its defaults set to.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
		public ItemSettingDefaultsEventArgs(IItem item, ItemType type)
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
