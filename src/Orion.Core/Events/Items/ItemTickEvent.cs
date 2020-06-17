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
using Orion.Core.Items;
using Serilog.Events;

namespace Orion.Core.Events.Items
{
    /// <summary>
    /// An event that occurs when an item tick (update) occurs. This event can be canceled.
    /// </summary>
    [Event("item-tick", LoggingLevel = LogEventLevel.Verbose)]
    public sealed class ItemTickEvent : ItemEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTickEvent"/> class with the specified
        /// <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item being ticked.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        public ItemTickEvent(IItem item) : base(item) { }
    }
}
