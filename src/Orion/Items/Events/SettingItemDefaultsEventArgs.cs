using System;
using System.ComponentModel;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.SettingItemDefaults"/> event.
    /// </summary>
    public sealed class SettingItemDefaultsEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the item that is having its defaults set.
        /// </summary>
        public IItem Item { get; }

        /// <summary>
        /// Gets or sets the <see cref="ItemType"/> that the item is having its defaults set to.
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingItemDefaultsEventArgs"/> class with the specified item
        /// and item type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="type">The item type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public SettingItemDefaultsEventArgs(IItem item, ItemType type) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Type = type;
        }
    }
}
