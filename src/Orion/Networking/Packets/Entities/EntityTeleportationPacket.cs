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
using Microsoft.Xna.Framework;
using Orion.Networking.Entities;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent to teleport an entity.
    /// </summary>
    public sealed class EntityTeleportationPacket : Packet {
        private EntityTeleportationType _teleportationType = EntityTeleportationType.Player;
        private byte _teleportationStyle;
        private short _entityIndex;
        private Vector2 _entityNewPosition;

        /// <inheritdoc />
        public override PacketType Type => PacketType.EntityTeleportation;

        /// <summary>
        /// Gets or sets the teleportation type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public EntityTeleportationType TeleportationType {
            get => _teleportationType;
            set {
                _teleportationType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the teleportation style.
        /// </summary>
        public byte TeleportationStyle {
            get => _teleportationStyle;
            set {
                _teleportationStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the entity index.
        /// </summary>
        public short EntityIndex {
            get => _entityIndex;
            set {
                _entityIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 EntityNewPosition {
            get => _entityNewPosition;
            set {
                _entityNewPosition = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={EntityIndex} to {EntityNewPosition} ({TeleportationType}_{TeleportationStyle})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var header = reader.ReadByte();
            TeleportationType = EntityTeleportationType.FromId((byte)(header & 3));
            TeleportationStyle = (byte)((header >> 2) & 3);

            EntityIndex = reader.ReadInt16();
            EntityNewPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            byte header = 0;
            header |= TeleportationType.Id;
            header |= (byte)((TeleportationStyle & 3) << 2);
            writer.Write(header);

            writer.Write(EntityIndex);
            writer.Write(EntityNewPosition);
        }
    }
}
