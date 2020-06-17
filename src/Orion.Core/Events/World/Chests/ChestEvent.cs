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
using Orion.Core.World.Chests;

namespace Orion.Core.Events.World.Chests
{
    /// <summary>
    /// Provides the base class for a chest-related event.
    /// </summary>
    public abstract class ChestEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChestEvent"/> class with the specified
        /// <paramref name="chest"/>.
        /// </summary>
        /// <param name="chest">The chest involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="chest"/> is <see langword="null"/>.</exception>
        protected ChestEvent(IChest chest)
        {
            Chest = chest ?? throw new ArgumentNullException(nameof(chest));
        }

        /// <summary>
        /// Gets the chest involved in the event.
        /// </summary>
        /// <value>The chest involved in the event.</value>
        public IChest Chest { get; }
    }
}
