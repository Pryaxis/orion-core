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
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// /Packet sent to teleport an entity.
    /// </summary>
    public sealed class EntityTeleportationPacket : Packet {
        /// <summary>
        /// Gets or sets the teleportation type.
        /// </summary>
        public EntityTeleportationType TeleportationType { get; set; }

        /// <summary>
        /// Gets or sets the teleportation style.
        /// </summary>
        public byte TeleportationStyle { get; set; }

        /// <summary>
        /// Gets or sets the entity index.
        /// </summary>
        public short EntityIndex { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 EntityNewPosition { get; set; }

        private protected override PacketType Type => PacketType.EntityTeleportation;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={EntityIndex} to {EntityNewPosition} ({TeleportationType}_{TeleportationStyle})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var header = reader.ReadByte();
            TeleportationType = (EntityTeleportationType)(header & 3);
            TeleportationStyle = (byte)((header >> 2) & 3);

            EntityIndex = reader.ReadInt16();
            EntityNewPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            byte header = 0;
            header |= (byte)((byte)TeleportationType & 3);
            header |= (byte)((TeleportationStyle & 3) << 2);
            writer.Write(header);

            writer.Write(EntityIndex);
            writer.Write(EntityNewPosition);
        }
    }
}
