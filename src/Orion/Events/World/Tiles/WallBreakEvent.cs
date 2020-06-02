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

namespace Orion.Events.World.Tiles {
    /// <summary>
    /// An event that occurs when a wall is being broken. This event can be canceled.
    /// </summary>
    [Event("wall-break")]
    public sealed class WallBreakEvent : TileEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="WallBreakEvent"/> class with the specified
        /// <paramref name="player"/>, coordinates, and failure status.
        /// </summary>
        /// <param name="player">The player, or <see langword="null"/> for none.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="isFailure">Whether the wall break attempt is a failure.</param>
        public WallBreakEvent(IPlayer? player, int x, int y, bool isFailure) : base(player, x, y) {
            IsFailure = isFailure;
        }

        /// <summary>
        /// Gets a value indicating whether the wall break attempt is a failure: i.e., whether the wall has not been
        /// fully broken yet.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the wall break attempt is a failure; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsFailure { get; }
    }
}
