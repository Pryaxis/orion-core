using System;
using System.ComponentModel;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.ItemSettingDefaults"/> event.
    /// </summary>
    public sealed class ItemSettingDefaultsEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the item that is having its defaults set.
        /// </summary>
        public IItem Item { get; }

        /// <summary>
        /// Gets or sets the <see cref="ItemType"/> that the item is having its defaults set to.
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSettingDefaultsEventArgs"/> class with the specified item
        /// and type.
        /// </summary>
        /// <param name="item">The item that is having its defaults set.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public ItemSettingDefaultsEventArgs(IItem item, ItemType type) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Type = type;
        }
    }
}
