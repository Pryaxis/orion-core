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
using System.Text;
using Orion.Events;
using Orion.Networking.Packets;
using Orion.Utils;
using Orion.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Represents a liquid transmitted over the network.
    /// </summary>
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
        /// Reads and returns a network liquid from the given stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="shouldSwapXY">A value indicating whether the X and Y values should be swapped.</param>
        /// <returns>The resulting network tile liquid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="PacketException">The liquid type was invalid.</exception>
        public static NetworkLiquid ReadFromStream(Stream stream, bool shouldSwapXY = false) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var reader = new BinaryReader(stream, Encoding.UTF8, true);
            var coord1 = reader.ReadInt16();
            var coord2 = reader.ReadInt16();
            return new NetworkLiquid {
                TileX = shouldSwapXY ? coord2 : coord1,
                TileY = shouldSwapXY ? coord1 : coord2,
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
        /// Writes the network liquid to the given stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="shouldSwapXY">A value indicating whether the X and Y values should be swapped.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public void WriteToStream(Stream stream, bool shouldSwapXY = false) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var writer = new BinaryWriter(stream, Encoding.UTF8, true);
            writer.Write(shouldSwapXY ? TileY : TileX);
            writer.Write(shouldSwapXY ? TileX : TileY);
            writer.Write(LiquidAmount);
            writer.Write((byte)LiquidType);
        }
    }
}
