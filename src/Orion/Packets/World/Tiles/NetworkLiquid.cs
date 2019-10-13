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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Utils;
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Represents a liquid transmitted over the network.
    /// </summary>
    public sealed class NetworkLiquid : IDirtiable {
        private short _tileX;
        private short _tileY;
        private byte _liquidAmount;
        private LiquidType _liquidType;

        /// <inheritdoc/>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX {
            get => _tileX;
            set {
                _tileX = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY {
            get => _tileY;
            set {
                _tileY = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the liquid amount.
        /// </summary>
        public byte LiquidAmount {
            get => _liquidAmount;
            set {
                _liquidAmount = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the liquid type.
        /// </summary>
        public LiquidType LiquidType {
            get => _liquidType;
            set {
                _liquidType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Reads and returns a network liquid from a <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="shouldSwapCoords">A value indicating whether the coordinates should be swapped.</param>
        /// <returns>The resulting network tile liquid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <see langword="null"/>.</exception>
        /// <exception cref="PacketException">The liquid type was invalid.</exception>
        public static NetworkLiquid ReadFromReader(BinaryReader reader, bool shouldSwapCoords = false) {
            if (reader is null) {
                throw new ArgumentNullException(nameof(reader));
            }

            var coord1 = reader.ReadInt16();
            var coord2 = reader.ReadInt16();
            return new NetworkLiquid {
                _tileX = shouldSwapCoords ? coord2 : coord1,
                _tileY = shouldSwapCoords ? coord1 : coord2,
                _liquidAmount = reader.ReadByte(),
                _liquidType = (LiquidType)reader.ReadByte()
            };
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{LiquidType} x{LiquidAmount} @ ({TileX}, {TileY})";

        /// <summary>
        /// Writes the network liquid to a <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="shouldSwapCoords">A value indicating whether the X and Y values should be swapped.</param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> is <see langword="null"/>.</exception>
        public void WriteToWriter(BinaryWriter writer, bool shouldSwapCoords = false) {
            if (writer is null) {
                throw new ArgumentNullException(nameof(writer));
            }

            writer.Write(shouldSwapCoords ? _tileY : _tileX);
            writer.Write(shouldSwapCoords ? _tileX : _tileY);
            writer.Write(_liquidAmount);
            writer.Write((byte)_liquidType);
        }
    }
}
