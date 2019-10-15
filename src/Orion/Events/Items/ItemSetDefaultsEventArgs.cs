// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using Orion.Items;

namespace Orion.Events.Items {
    /// <summary>
    /// Provides data for the <see cref="IItemService.ItemSetDefaults"/> event.
    /// </summary>
    [EventArgs("item-defaults")]
    public sealed class ItemSetDefaultsEventArgs : ItemEventArgs, ICancelable {
        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Gets or sets the item type that the item's defaults are being set to.
        /// </summary>
        /// <value>The item type that the item's defaults are being set to.</value>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSetDefaultsEventArgs"/> class with the specified item and
        /// item type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="itemType">The item type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public ItemSetDefaultsEventArgs(IItem item, ItemType itemType) : base(item) {
            ItemType = itemType;
        }
    }
}
