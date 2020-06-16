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
using Orion.Core.Players;
using Orion.Core.World;
using Orion.Core.World.Tiles;

namespace Orion.Core.Events.World.Tiles {
    /// <summary>
    /// An event that occurs when a tile's liquid is being set. This event can be canceled.
    /// </summary>
    [Event("tile-liquid")]
    public sealed class TileLiquidEvent : TileEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileLiquidEvent"/> class with the specified
        /// <paramref name="world"/>, <paramref name="player"/>, tile coordinates, <paramref name="liquidAmount"/>, and
        /// <paramref name="liquid"/>.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <param name="player">The player setting the tile's liquid, or <see langword="null"/> for none.</param>
        /// <param name="x">The tile's X coordinate.</param>
        /// <param name="y">The tile's Y coordinate.</param>
        /// <param name="liquidAmount">The tile's liquid amount.</param>
        /// <param name="liquid">The tile's liquid.</param>
        /// <exception cref="ArgumentNullException"><paramref name="world"/> is <see langword="null"/>.</exception>
        public TileLiquidEvent(IWorld world, IPlayer? player, int x, int y, byte liquidAmount, Liquid liquid)
                : base(world, player, x, y) {
            LiquidAmount = liquidAmount;
            Liquid = liquid;
        }

        /// <summary>
        /// Gets the tile's liquid amount. This ranges from <c>0</c> to <c>255</c>.
        /// </summary>
        /// <value>The tile's liquid amount.</value>
        public byte LiquidAmount { get; }

        /// <summary>
        /// Gets the tile's liquid.
        /// </summary>
        /// <value>The tile's liquid.</value>
        public Liquid Liquid { get; }
    }
}
