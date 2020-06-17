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
using Orion.Core.World;

namespace Orion.Core.Events.World
{
    /// <summary>
    /// Provides the base class for a world-related event.
    /// </summary>
    public abstract class WorldEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldEvent"/> class with the specified
        /// <paramref name="world"/>.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="world"/> is <see langword="null"/>.</exception>
        protected WorldEvent(IWorld world)
        {
            World = world ?? throw new ArgumentNullException(nameof(world));
        }

        /// <summary>
        /// Gets the world involved in the event.
        /// </summary>
        /// <value>The world involved in the event.</value>
        public IWorld World { get; }
    }
}
