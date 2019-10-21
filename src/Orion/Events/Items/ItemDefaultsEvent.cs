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
using Orion.Utils;

namespace Orion.Events.Items {
    /// <summary>
    /// An event that occurs when an item's defaults are being set. This event can be canceled.
    /// </summary>
    /// <remarks>
    /// This event occurs when an item is being created, which can happen:
    /// <list type="bullet">
    /// <item>
    /// <description>When the server starts up, where all items are initialized.</description>
    /// </item>
    /// <item>
    /// <description>
    /// When an item is loaded as part of an abstract container (inventory, chest, item frame, etc.)
    /// </description>
    /// </item>
    /// <item>
    /// <description>When an item spawns in the world.</description>
    /// </item>
    /// </list>
    /// </remarks>
    [EventArgs("item-defaults")]
    public sealed class ItemDefaultsEvent : ItemEvent, ICancelable, IDirtiable {
        private ItemType _itemType;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the item type that the item's defaults are being set to.
        /// </summary>
        /// <value>The item type that the item's defaults are being set to.</value>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDefaultsEvent"/> class with the specified item and item
        /// type.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="itemType">The item type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public ItemDefaultsEvent(IItem item, ItemType itemType) : base(item) {
            _itemType = itemType;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;
    }
}
