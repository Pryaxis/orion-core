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
using Orion.Items;

namespace Orion.Events.Items {
    /// <summary>
    /// Provides the base class for an item-related event.
    /// </summary>
    public abstract class ItemEvent : Event {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemEvent"/> class with the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected ItemEvent(IItem item) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        /// <summary>
        /// Gets the item involved in the event.
        /// </summary>
        /// <value>The item involved in the event.</value>
        public IItem Item { get; }
    }
}
