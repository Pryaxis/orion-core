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

using Orion.Players;
using Orion.World.Tiles;

namespace Orion.Events.World.Tiles {
    /// <summary>
    /// An event that occurs when a block is being placed. This event can be canceled.
    /// </summary>
    [Event("block-place")]
    public sealed class BlockPlaceEvent : TileEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockPlaceEvent"/> class with the specified
        /// <paramref name="player"/>, coordinates, block <paramref name="id"/>, <paramref name="style"/>, and
        /// replacement status.
        /// </summary>
        /// <param name="player">The player, or <see langword="null"/> for none.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="id">The block ID.</param>
        /// <param name="style">The style.</param>
        /// <param name="isReplacement">Whether the block placement is a replacement.</param>
        public BlockPlaceEvent(IPlayer? player, int x, int y, BlockId id, int style, bool isReplacement)
                : base(player, x, y) {
            Id = id;
            Style = style;
            IsReplacement = isReplacement;
        }

        /// <summary>
        /// Gets the block ID being placed.
        /// </summary>
        /// <value>The block ID being placed.</value>
        public BlockId Id { get; }

        /// <summary>
        /// Gets the style of the block being placed.
        /// </summary>
        /// <value>The style of the block being placed.</value>
        public int Style { get; }

        /// <summary>
        /// Gets a value indicating whether the block placement is a replacement: i.e., whether an existing block is
        /// being replaced by another block.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the block placement is a replacement; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsReplacement { get; }
    }
}
