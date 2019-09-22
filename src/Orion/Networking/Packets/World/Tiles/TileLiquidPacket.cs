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
using Orion.Networking.World.Tiles;

namespace Orion.Networking.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to set a tile's liquid.
    /// </summary>
    [PublicAPI]
    public sealed class TileLiquidPacket : Packet {
        [NotNull] private NetworkLiquid _tileLiquid = new NetworkLiquid();

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || TileLiquid.IsDirty;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TileLiquid;

        /// <summary>
        /// Gets or sets the tile's liquid.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        [NotNull]
        public NetworkLiquid TileLiquid {
            get => _tileLiquid;
            set {
                _tileLiquid = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            TileLiquid.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{TileLiquid}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _tileLiquid = NetworkLiquid.ReadFromReader(reader);
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            _tileLiquid.WriteToWriter(writer);
        }
    }
}
