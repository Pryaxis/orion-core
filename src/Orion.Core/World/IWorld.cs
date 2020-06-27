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

using Orion.Core.Events.World;
using Orion.Core.Events.World.Tiles;
using Orion.Core.World.Tiles;

namespace Orion.Core.World
{
    /// <summary>
    /// Represents a world. Provides access to the tiles and publishes world and tile-related events.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// The world is responsible for publishing the following world and tile-related events:
    /// <list type="bullet">
    /// <item><description><see cref="WorldLoadedEvent"/></description></item>
    /// <item><description><see cref="WorldSaveEvent"/></description></item>
    /// <item><description><see cref="BlockBreakEvent"/></description></item>
    /// <item><description><see cref="BlockPlaceEvent"/></description></item>
    /// <item><description><see cref="WallBreakEvent"/></description></item>
    /// <item><description><see cref="WallPlaceEvent"/></description></item>
    /// <item><description><see cref="TileSquareEvent"/></description></item>
    /// <item><description><see cref="TileLiquidEvent"/></description></item>
    /// <item><description><see cref="WiringActivateEvent"/></description></item>
    /// <item><description><see cref="BlockPaintEvent"/></description></item>
    /// <item><description><see cref="WallPaintEvent"/></description></item>
    /// </list>
    /// </remarks>
    [Service(ServiceScope.Singleton)]
    public interface IWorld : ITileSlice
    {
        /// <summary>
        /// Gets the world's name.
        /// </summary>
        /// <value>The world's name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the world's evil.
        /// </summary>
        /// <value>The world's evil.</value>
        public WorldEvil Evil { get; set; }

        /// <summary>
        /// Gets or sets the world's difficulty.
        /// </summary>
        /// <value>The world's difficulty.</value>
        public WorldDifficulty Difficulty { get; set; }
    }
}
