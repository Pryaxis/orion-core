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
using Orion.World;

namespace Orion.Events.World {
    /// <summary>
    /// An event that occurs when the world is saving. This event can be canceled.
    /// </summary>
    [Event("world-save")]
    public sealed class WorldSaveEvent : WorldEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldSaveEvent"/> class with the specified
        /// <paramref name="world"/>.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="world"/> is <see langword="null"/>.</exception>
        public WorldSaveEvent(IWorld world) : base(world) { }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
