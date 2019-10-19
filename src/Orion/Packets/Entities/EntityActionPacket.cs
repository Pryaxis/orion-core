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

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent to perform an entity action. See <see cref="Entities.EntityAction"/> for the list of possible actions.
    /// </summary>
    public sealed class EntityActionPacket : Packet {
        private byte _entityIndex;
        private EntityAction _action;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.EntityAction;

        /// <summary>
        /// Gets or sets the entity index.
        /// </summary>
        /// <value>The entity index.</value>
        public byte EntityIndex {
            get => _entityIndex;
            set {
                _entityIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the entity action.
        /// </summary>
        /// <value>The entity action.</value>
        public EntityAction Action {
            get => _action;
            set {
                _action = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _entityIndex = reader.ReadByte();
            _action = (EntityAction)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_entityIndex);
            writer.Write((byte)_action);
        }
    }
}
