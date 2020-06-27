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

namespace Orion.Core.Events.World.Tiles
{
    /// <summary>
    /// An event that occurs when a block is being painted. This event can be canceled.
    /// </summary>
    [Event("block-paint")]
    public sealed class BlockPaintEvent : TileEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockPaintEvent"/> class with the specified
        /// <paramref name="world"/>, <paramref name="player"/>, coordinates, and block <paramref name="color"/>.
        /// </summary>
        /// <param name="world">The world involved in the event.</param>
        /// <param name="player">The player painting the block, or <see langword="null"/> for none.</param>
        /// <param name="x">The block's X coordinate.</param>
        /// <param name="y">The block's Y coordinate.</param>
        /// <param name="color">The block color being painted.</param>
        /// <exception cref="ArgumentNullException"><paramref name="world"/> is <see langword="null"/>.</exception>
        public BlockPaintEvent(IWorld world, IPlayer? player, int x, int y, PaintColor color)
            : base(world, player, x, y)
        {
            Color = color;
        }

        /// <summary>
        /// Gets the block color being painted.
        /// </summary>
        /// <value>The block color being painted.</value>
        public PaintColor Color { get; }
    }
}
