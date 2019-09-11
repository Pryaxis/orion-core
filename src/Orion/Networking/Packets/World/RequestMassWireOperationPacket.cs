﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the client to the server to request a mass wire operation.
    /// </summary>
    public sealed class RequestMassWireOperationPacket : Packet {
        /// <summary>
        /// Gets or sets the starting tile's X position.
        /// </summary>
        public short StartTileX { get; set; }

        /// <summary>
        /// Gets or sets the starting tile's Y position.
        /// </summary>
        public short StartTileY { get; set; }

        /// <summary>
        /// Gets or sets the ending tile's X position.
        /// </summary>
        public short EndTileX { get; set; }

        /// <summary>
        /// Gets or sets the ending tile's X position.
        /// </summary>
        public short EndTileY { get; set; }

        /// <summary>
        /// Gets or sets the wire operation type.
        /// </summary>
        public MassWireOperations WireOperations { get; set; }

        private protected override PacketType Type => PacketType.RequestMassWireOperation;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[{WireOperations:F} from ({StartTileX}, {StartTileY}) to ({EndTileX}, {EndTileY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            StartTileX = reader.ReadInt16();
            StartTileY = reader.ReadInt16();
            EndTileX = reader.ReadInt16();
            EndTileY = reader.ReadInt16();
            WireOperations = (MassWireOperations)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(StartTileX);
            writer.Write(StartTileY);
            writer.Write(EndTileX);
            writer.Write(EndTileY);
            writer.Write((byte)WireOperations);
        }
    }
}