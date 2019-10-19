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
using System.IO;
using Orion.Utils;
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Represents a liquid transmitted over the network.
    /// </summary>
    public sealed class NetworkLiquid : IDirtiable {
        private short _x;
        private short _y;
        private byte _amount;
        private LiquidType _liquidType;

        /// <inheritdoc/>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        /// <value>The tile's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        /// <value>The tile's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the liquid amount.
        /// </summary>
        /// <value>The liquid amount.</value>
        public byte Amount {
            get => _amount;
            set {
                _amount = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the liquid type.
        /// </summary>
        /// <value>The liquid type.</value>
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
        /// <param name="shouldSwapCoords">
        /// <see langword="true"/> if the coordinates should be swapped; otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>The resulting network liquid.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <see langword="null"/>.</exception>
        /// <exception cref="PacketException">The liquid type was invalid.</exception>
        public static NetworkLiquid ReadFromReader(BinaryReader reader, bool shouldSwapCoords = false) {
            if (reader is null) {
                throw new ArgumentNullException(nameof(reader));
            }

            var coord1 = reader.ReadInt16();
            var coord2 = reader.ReadInt16();
            return new NetworkLiquid {
                _x = shouldSwapCoords ? coord2 : coord1,
                _y = shouldSwapCoords ? coord1 : coord2,
                _amount = reader.ReadByte(),
                _liquidType = (LiquidType)reader.ReadByte()
            };
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;

        /// <summary>
        /// Writes the network liquid to a <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="shouldSwapCoords">
        /// <see langword="true"/> if the coordinates should be swapped; otherwise, <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> is <see langword="null"/>.</exception>
        public void WriteToWriter(BinaryWriter writer, bool shouldSwapCoords = false) {
            if (writer is null) {
                throw new ArgumentNullException(nameof(writer));
            }

            writer.Write(shouldSwapCoords ? _y : _x);
            writer.Write(shouldSwapCoords ? _x : _y);
            writer.Write(_amount);
            writer.Write((byte)_liquidType);
        }
    }
}
