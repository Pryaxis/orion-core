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
using System.Diagnostics.Contracts;
using Orion.Core.Utils;

namespace Orion.Core.Entities
{
    /// <summary>
    /// Represents an annotatable Terraria entity.
    /// </summary>
    /// <remarks>
    /// <para>Implementations must be thread-safe.</para>
    /// <para>
    /// Many Terraria objects are entities and have common properties such as position, velocity, etc. Entities are
    /// annotatable, allowing consumers to easily attach custom state to them.
    /// </para>
    /// <para>
    /// Technically, an entity might actually refer to an entity <i>slot</i>, as entity objects are reused for entities
    /// which occupy the same index in the world. This means that certain state, such as annotations, may be left over
    /// from the previous entity.
    /// </para>
    /// <para>
    /// There are three types of entities:
    /// <list type="number">
    /// <item>
    /// <description>Entities which are abstract: they cannot affect the world.</description>
    /// </item>
    /// <item>
    /// <description>
    /// Entities which are concrete but <i>not</i> active: the entity does not exist in the world.
    /// </description>
    /// </item>
    /// <item>
    /// <description>Entities which are concrete <i>and</i> active: the entity exists in the world.</description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// These types can be differentiated using the <see cref="IEntityExtensions.IsConcrete"/> extension method and the
    /// <see cref="IsActive"/> property.
    /// </para>
    /// </remarks>
    public interface IEntity : IAnnotatable
    {
        /// <summary>
        /// Gets the entity's index. A negative value indicates that the entity is abstract.
        /// </summary>
        /// <value>The entity's index.</value>
        public int Index { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active: i.e., whether the entity exists in the world.
        /// </summary>
        /// <value><see langword="true"/> if the entity is active; otherwise, <see langword="false"/>.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the entity's name.
        /// </summary>
        /// <value>The entity's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the entity's position, in pixels.
        /// </summary>
        /// <value>The entity's position, in pixels.</value>
        public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the entity's velocity, in pixels per tick.
        /// </summary>
        /// <value>The entity's velocity, in pixels per tick.</value>
        public Vector2f Velocity { get; set; }

        /// <summary>
        /// Gets or sets the entity's size, in pixels.
        /// </summary>
        /// <value>The entity's size, in pixels.</value>
        public Vector2f Size { get; set; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IEntity"/> interface.
    /// </summary>
    public static class IEntityExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the entity is concrete: i.e., whether the entity <i>can</i> exist in the
        /// world.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><see langword="true"/> if the entity is concrete; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is <see langword="null"/>.</exception>
        [Pure]
        public static bool IsConcrete(this IEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return entity.Index >= 0;
        }
    }
}
