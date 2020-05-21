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
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Represents a Terraria entity. There are three types of entities:
    /// </summary>
    public interface IEntity : IAnnotatable {
        /// <summary>
        /// Gets the entity's index. A value of <c>-1</c> indicates that the entity is abstract.
        /// </summary>
        /// <value>The entity's index.</value>
        int Index { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active: i.e., whether the entity exists in the world.
        /// </summary>
        /// <value><see langword="true"/> if the entity is active; otherwise, <see langword="false"/>.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the entity's name.
        /// </summary>
        /// <value>The entity's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the entity's position, in pixels.
        /// </summary>
        /// <value>The entity's position, in pixels.</value>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the entity's velocity, in pixels per tick.
        /// </summary>
        /// <value>The entity's velocity, in pixels per tick.</value>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the entity's size, in pixels.
        /// </summary>
        /// <value>The entity's size, in pixels.</value>
        Vector2 Size { get; set; }
    }
}
