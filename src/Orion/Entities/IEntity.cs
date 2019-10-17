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
using Microsoft.Xna.Framework;
using Orion.Utils;

namespace Orion.Entities {
    /// <summary>
    /// Represents a Terraria entity.
    /// </summary>
    /// <remarks>
    /// Entities are the base of many Terraria objects and have common properties such as <see cref="Position"/>,
    /// <see cref="Velocity"/>, etc. They are annotatable, allowing consumers to easily attach extra state to them.
    /// <para/>
    /// 
    /// There are two types of entities:
    /// <list type="bullet">
    /// <item>
    /// <description>Entities which are abstract and do not affect the game state.</description>
    /// </item>
    /// <item>
    /// <description>Entities which are concrete and affect the game state.</description>
    /// </item>
    /// </list>
    /// 
    /// Care must be taken to differentiate the two using the <see cref="Index"/> and <see cref="IsActive"/> properties.
    /// </remarks>
    public interface IEntity : IAnnotatable {
        /// <summary>
        /// Gets the entity's index. A value of <c>-1</c> indicates that the entity is abstract.
        /// </summary>
        /// <value>The entity's index.</value>
        int Index { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active: i.e., whether the entity is concrete and
        /// exists in the world.
        /// </summary>
        /// <value><see langword="true"/> if the entity is active; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// This property is only meaningful if the entity is concrete. Setting this property to <see langword="false"/>
        /// allows you to remove the entity from the world.
        /// </remarks>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the entity's name.
        /// </summary>
        /// <value>The entity's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the entity's position. The components are pixels.
        /// </summary>
        /// <value>The entity's position.</value>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the entity's velocity. The components are pixels.
        /// </summary>
        /// <value>The entity's velocity.</value>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the entity's size. The components are pixels per tick.
        /// </summary>
        /// <value>The entity's size.</value>
        Vector2 Size { get; set; }
    }
}
