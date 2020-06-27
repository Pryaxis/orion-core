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
using Orion.Core.Entities;

namespace Orion.Core.World.TileEntities
{
    /// <summary>
    /// Represents an annotatable Terraria tile entity.
    /// </summary>
    /// <remarks>
    /// Implementations must be thread-safe.
    /// 
    /// Many Terraria tile-based objects are tile entities and have common properties such as position. Tile entities
    /// are annotatable, allowing consumers to easily attach custom state to them.
    /// 
    /// There are three types of tile entities:
    /// <list type="number">
    /// <item>
    /// <description>Tile entities which are abstract: they cannot affect the world.</description>
    /// </item>
    /// <item>
    /// <description>
    /// Tile entities which are concrete but <i>not</i> active: the entity does not exist in the world.
    /// </description>
    /// </item>
    /// <item>
    /// <description>Tile entities which are concrete <i>and</i> active: the entity exists in the world.</description>
    /// </item>
    /// </list>
    /// 
    /// These types can be differentiated using the <see cref="ITileEntityExtensions.IsConcrete"/> extension method and
    /// the <see cref="IsActive"/> property.
    /// </remarks>
    public interface ITileEntity : IAnnotatable
    {
        /// <summary>
        /// Gets the tile entity's index. A negative value indicates that the tile entity is abstract.
        /// </summary>
        /// <value>The tile entity's index.</value>
        public int Index { get; }

        /// <summary>
        /// Gets a value indicating whether the tile entity is active: i.e., whether the tile entity exists in the
        /// world.
        /// </summary>
        /// <value><see langword="true"/> if the tile entity is active; otherwise, <see langword="false"/>.</value>
        public bool IsActive { get; }

        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        /// <value>The tile entity's X coordinate.</value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        /// <value>The tile entity's Y coordinate.</value>
        public int Y { get; set; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="ITileEntity"/> interface.
    /// </summary>
    public static class ITileEntityExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the tile entity is concrete: i.e., whether the tile entity <i>can</i>
        /// exist in the world.
        /// </summary>
        /// <param name="tileEntity">The tile entity.</param>
        /// <returns>
        /// <see langword="true"/> if the tile entity is concrete; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntity"/> is <see langword="null"/>.</exception>
        [Pure]
        public static bool IsConcrete(this ITileEntity tileEntity)
        {
            if (tileEntity is null)
            {
                throw new ArgumentNullException(nameof(tileEntity));
            }

            return tileEntity.Index >= 0;
        }
    }
}
