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

namespace Orion.Core.World
{
    /// <summary>
    /// Represents a Terraria world.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// </remarks>
    public interface IWorld : ITileSlice, IAnnotatable
    {
        /// <summary>
        /// Gets the world's name.
        /// </summary>
        /// <value>The world's name.</value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the world's difficulty.
        /// </summary>
        /// <value>The world's difficulty.</value>
        WorldDifficulty Difficulty { get; set; }
    }
}
