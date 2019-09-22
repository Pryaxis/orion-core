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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Orion.Networking.Packets;
using Orion.Utils;
using Orion.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Represents a liquid transmitted over the network.
    /// </summary>
    [PublicAPI]
    public sealed class NetworkLiquid : IDirtiable {
        private short _tileX;
        private short _tileY;
        private byte _liquidAmount;
        private LiquidType _liquidType;

        /// <inheritdoc />
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
        /// Reads and returns a network liquid from the given reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="shouldSwapCoords">A value indicating whether the coordinates should be swapped.</param>
        /// <returns>The resulting network tile liquid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <c>null</c>.</exception>
        /// <exception cref="PacketException">The liquid type was invalid.</exception>
        [NotNull]
        public static NetworkLiquid ReadFromReader([NotNull] BinaryReader reader, bool shouldSwapCoords = false) {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var coord1 = reader.ReadInt16();
            var coord2 = reader.ReadInt16();
            return new NetworkLiquid {
                TileX = shouldSwapCoords ? coord2 : coord1,
                TileY = shouldSwapCoords ? coord1 : coord2,
                LiquidAmount = reader.ReadByte(),
                LiquidType = (LiquidType)reader.ReadByte()
            };
        }

        /// <inheritdoc />
        public void Clean() {
            IsDirty = false;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{LiquidType} x{LiquidAmount} @ ({TileX}, {TileY})";

        /// <summary>
        /// Writes the network liquid to the given writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="shouldSwapCoords">A value indicating whether the X and Y values should be swapped.</param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> is <c>null</c>.</exception>
        public void WriteToWriter([NotNull] BinaryWriter writer, bool shouldSwapCoords = false) {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            writer.Write(shouldSwapCoords ? TileY : TileX);
            writer.Write(shouldSwapCoords ? TileX : TileY);
            writer.Write(LiquidAmount);
            writer.Write((byte)LiquidType);
        }
    }
}
