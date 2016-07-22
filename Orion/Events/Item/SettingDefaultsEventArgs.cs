using System.ComponentModel;
using Orion.Interfaces;

namespace Orion.Events.Item
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.SettingDefaults"/> event.
	/// </summary>
	public class SettingDefaultsEventArgs : HandledEventArgs
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
		/// Initializes a new instance of the <see cref="SettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> that is having its defaults set.</param>
		/// <param name="type">The type ID that the <see cref="IItem"/> is having its defaults set to.</param>
		public SettingDefaultsEventArgs(IItem item, int type)
		{
			Item = item;
			Type = type;
		}
	}
}
