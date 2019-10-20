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

using System.IO;
using Microsoft.Xna.Framework;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent to teleport an entity.
    /// </summary>
    /// <remarks>
    /// While entities can be teleported by modifying their positions, this packet creates the visual and audio effects
    /// of the teleportation.
    /// </remarks>
    public sealed class EntityTeleportationPacket : Packet {
        private EntityTeleportationType _teleportationType;
        private byte _style;
        private short _entityIndex;
        private Vector2 _newPosition;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.EntityTeleportation;

        /// <summary>
        /// Gets or sets the entity's teleportation type.
        /// </summary>
        /// <value>The entity's teleportation type.</value>
        public EntityTeleportationType TeleportationType {
            get => _teleportationType;
            set {
                _teleportationType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the entity's teleportation style, or how the teleportation looks.
        /// </summary>
        /// <value>The entity's teleportation style.</value>
        public byte Style {
            get => _style;
            set {
                _style = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the entity index.
        /// </summary>
        /// <value>The entity index.</value>
        public short EntityIndex {
            get => _entityIndex;
            set {
                _entityIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the entity's new position. The components are pixels.
        /// </summary>
        /// <value>The entity's new position.</value>
        public Vector2 NewPosition {
            get => _newPosition;
            set {
                _newPosition = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var header = reader.ReadByte();
            _teleportationType = (EntityTeleportationType)(header & 3);
            _style = (byte)((header >> 2) & 3);
            _entityIndex = reader.ReadInt16();
            _newPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            byte header = 0;
            header |= (byte)((byte)_teleportationType & 3);
            header |= (byte)((_style & 3) << 2);
            writer.Write(header);
            writer.Write(_entityIndex);
            writer.Write(in _newPosition);
        }
    }
}
