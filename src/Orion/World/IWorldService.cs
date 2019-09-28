// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Orion.World.Tiles;

namespace Orion.World {
    /// <summary>
    /// Represents a world service. Provides access to world-related events and methods.
    /// </summary>
    public interface IWorldService : IService {
        /// <summary>
        /// Gets a reference to the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>A reference to the tile.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="x"/> or <paramref name="y"/> are out of range.
        /// </exception>
        ref Tile this[int x, int y] { get; }
    }
}
