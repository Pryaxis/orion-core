using System;
using System.ComponentModel;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.UpdatingItem"/> event.
    /// </summary>
    public sealed class UpdatingItemEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the item that is being updated.
        /// </summary>
        public IItem Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingItemEventArgs"/> class with the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public UpdatingItemEventArgs(IItem item) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}
