// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Destructurama.Attributed;
using Orion.Items;
using Serilog.Events;

namespace Orion.Events.Items {
    /// <summary>
    /// An event that occurs when an item's defaults are being set: i.e., when an item's stats are being initialized.
    /// This event can be canceled.
    /// </summary>
    [Event("item-defaults", LoggingLevel = LogEventLevel.Verbose)]
    public sealed class ItemDefaultsEvent : ItemEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDefaultsEvent"/> class with the specified
        /// <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item whose defaults are being set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public ItemDefaultsEvent(IItem item) : base(item) { }

        /// <summary>
        /// Gets or sets the item ID that the item's defaults are being set to.
        /// </summary>
        /// <value>The item ID that the item's defaults are being set to.</value>
        public ItemId Id { get; set; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
