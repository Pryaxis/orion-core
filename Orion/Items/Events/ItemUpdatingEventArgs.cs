using System;
using System.ComponentModel;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.ItemUpdating"/> event.
    /// </summary>
    public sealed class ItemUpdatingEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the item that was updated.
        /// </summary>
        public IItem Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemUpdatingEventArgs"/> class with the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public ItemUpdatingEventArgs(IItem item) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}
