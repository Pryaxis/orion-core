using System;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.UpdatedItem"/> event.
    /// </summary>
    public sealed class UpdatedItemEventArgs : EventArgs {
        /// <summary>
        /// Gets the item that was updated.
        /// </summary>
        public IItem Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatedItemEventArgs"/> class with the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public UpdatedItemEventArgs(IItem item) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}
