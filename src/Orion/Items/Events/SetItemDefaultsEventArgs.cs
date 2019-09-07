using System;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.SetItemDefaults"/> event.
    /// </summary>
    public sealed class SetItemDefaultsEventArgs : EventArgs {
        /// <summary>
        /// Gets the item that had its defaults set.
        /// </summary>
        public IItem Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetItemDefaultsEventArgs"/> class with the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public SetItemDefaultsEventArgs(IItem item) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}
