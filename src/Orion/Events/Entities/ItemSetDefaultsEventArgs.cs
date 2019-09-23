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

using JetBrains.Annotations;
using Orion.Entities;

namespace Orion.Events.Entities {
    /// <summary>
    /// Provides data for the <see cref="IItemService.ItemSetDefaults"/> event.
    /// </summary>
    [PublicAPI]
    public sealed class ItemSetDefaultsEventArgs : ItemEventArgs, ICancelable {
        /// <inheritdoc />
        public bool IsCanceled { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <inheritdoc />
        internal ItemSetDefaultsEventArgs([NotNull] IItem item, ItemType itemType) : base(item) {
            ItemType = itemType;
        }
    }
}
