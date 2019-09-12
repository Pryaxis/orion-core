// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.ComponentModel;

namespace Orion.Items.Events {
    /// <summary>
    /// Provides data for the <see cref="IItemService.SettingItemDefaults"/> handlers.
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
