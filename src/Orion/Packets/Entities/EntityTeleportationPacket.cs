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

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent to teleport an entity.
    /// </summary>
    public sealed class EntityTeleportationPacket : Packet {
        private EntityTeleportationType _entityTeleportationType;
        private byte _entityTeleportationStyle;
        private short _entityIndex;
        private Vector2 _entityNewPosition;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.EntityTeleportation;

        /// <summary>
        /// Gets or sets the entity teleportation type.
        /// </summary>
        public EntityTeleportationType EntityTeleportationType {
            get => _entityTeleportationType;
            set {
                _entityTeleportationType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the entity teleportation style, or how the teleportation looks.
        /// </summary>
        public byte EntityTeleportationStyle {
            get => _entityTeleportationStyle;
            set {
                _entityTeleportationStyle = value;
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
        /// Gets or sets the entity's new position. The components are pixel-based.
        /// </summary>
        public Vector2 EntityNewPosition {
            get => _entityNewPosition;
            set {
                _entityNewPosition = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={EntityIndex} to {EntityNewPosition} ({EntityTeleportationType}_{EntityTeleportationStyle})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var header = reader.ReadByte();
            _entityTeleportationType = (EntityTeleportationType)(header & 3);
            _entityTeleportationStyle = (byte)((header >> 2) & 3);
            _entityIndex = reader.ReadInt16();
            _entityNewPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            byte header = 0;
            header |= (byte)((byte)_entityTeleportationType & 3);
            header |= (byte)((_entityTeleportationStyle & 3) << 2);
            writer.Write(header);
            writer.Write(_entityIndex);
            writer.Write(in _entityNewPosition);
        }
    }
}
