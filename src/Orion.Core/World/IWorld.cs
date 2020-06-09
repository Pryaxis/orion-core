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

using Orion.Core.Entities;
using Orion.Core.World.Tiles;

namespace Orion.Core.World {
    /// <summary>
    /// Represents a Terraria world.
    /// </summary>
    public interface IWorld : IAnnotatable {
        /// <summary>
        /// Gets a reference to the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>A reference to the tile at the given coordinates.</returns>
        ref Tile this[int x, int y] { get; }

        /// <summary>
        /// Gets the world's width.
        /// </summary>
        /// <value>The world's width.</value>
        int Width { get; }

        /// <summary>
        /// Gets the world's height.
        /// </summary>
        /// <value>The world's height.</value>
        int Height { get; }

        /// <summary>
        /// Gets the world's name.
        /// </summary>
        /// <value>The world's name.</value>
        string Name { get; }
    }
}
