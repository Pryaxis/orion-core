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

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to set a tile's liquid.
    /// </summary>
    /// <remarks>This packet is sent when a player places a liquid.</remarks>
    public sealed class TileLiquidPacket : Packet {
        private NetworkLiquid _liquid = new NetworkLiquid();

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _liquid.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.TileLiquid;

        /// <summary>
        /// Gets or sets the tile's liquid.
        /// </summary>
        /// <value>The tile's liquid.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkLiquid Liquid {
            get => _liquid;
            set {
                _liquid = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _liquid.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _liquid = NetworkLiquid.ReadFromReader(reader);

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            _liquid.WriteToWriter(writer);
    }
}
