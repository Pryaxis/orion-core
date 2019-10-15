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
    /// Provides data for the <see cref="IItemService.ItemUpdate"/> event.
    /// </summary>
    [EventArgs("item-update")]
    public sealed class ItemUpdateEventArgs : ItemEventArgs, ICancelable {
        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemUpdateEventArgs"/> class with the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public ItemUpdateEventArgs(IItem item) : base(item) { }
    }
}
