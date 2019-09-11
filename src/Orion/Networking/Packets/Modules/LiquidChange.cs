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

using System.Diagnostics.CodeAnalysis;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Represents a liquid change in a <see cref="LiquidChangesModule"/>.
    /// </summary>
    public struct LiquidChange {
        /// <summary>
        /// Gets the tile's X coordinate.
        /// </summary>
        public short TileX { get; }

        /// <summary>
        /// Gets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; }

        /// <summary>
        /// Gets the liquid amount.
        /// </summary>
        public byte LiquidAmount { get; }

        /// <summary>
        /// Gets the liquid type.
        /// </summary>
        public LiquidType LiquidType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidChange"/> structure with the given coordinates, liquid
        /// amount, and liquid type.
        /// </summary>
        /// <param name="tileX">The tile's X coordinate.</param>
        /// <param name="tileY">The tile's Y coordinate.</param>
        /// <param name="liquidAmount">The liquid amount.</param>
        /// <param name="liquidType">The liquid type.</param>
        public LiquidChange(short tileX, short tileY, byte liquidAmount, LiquidType liquidType) {
            TileX = tileX;
            TileY = tileY;
            LiquidAmount = liquidAmount;
            LiquidType = liquidType;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{LiquidType} x{LiquidAmount} @ ({TileX}, {TileY})";
    }
}
