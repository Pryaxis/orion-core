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

using Orion.Utils;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a (generalized) Terraria tile entity.
    /// </summary>
    /// <remarks>
    /// Tile entities are tiles that require extra functionality. For example, chests are tile entities, since items
    /// need to be stored, and item frames are tile entities since the item needs to be stored.
    /// 
    /// There are two types of tile entities:
    /// <list type="bullet">
    /// <item>
    /// <description>Tile entities which are abstract and do not affect the game state.</description>
    /// </item>
    /// <item>
    /// <description>Tile entities which are concrete and affect the game state.</description>
    /// </item>
    /// </list>
    /// 
    /// Care must be taken to differentiate the two using the <see cref="Index"/> and <see cref="IsActive"/> properties.
    /// </remarks>
    public interface ITileEntity : IAnnotatable {
        /// <summary>
        /// Gets the tile entity's type.
        /// </summary>
        /// <value>The tile entity's type.</value>
        TileEntityType Type { get; }

        /// <summary>
        /// Gets the tile entity's index. A value of <c>-1</c> indicates that the entity is abstract.
        /// </summary>
        /// <value>The tile entity's index.</value>
        int Index { get; }
        
        /// <summary>
        /// Gets a value indicating whether the tile entity is active: i.e., whether the entity is concrete and exists
        /// in the world.
        /// </summary>
        /// <value><see langword="true"/> if the tile entity is active; otherwise, <see langword="false"/>.</value>
        /// <remarks>This property is only meaningful if the tile entity is concrete.</remarks>
        bool IsActive { get; }

        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        /// <value>The tile entity's X coordinate.</value>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        /// <value>The tile entity's Y coordinate.</value>
        int Y { get; set; }
    }
}
