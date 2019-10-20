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
using Orion.Npcs;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the client to the server to release an NPC.
    /// </summary>
    /// <remarks>This packet is sent when a player "uses" an NPC item.</remarks>
    public sealed class NpcReleasePacket : Packet {
        private Vector2 _position;
        private NpcType _npcType = NpcType.None;
        private byte _style;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcRelease;

        /// <summary>
        /// Gets or sets the NPC's position. The components are pixels.
        /// </summary>
        /// <value>The NPC's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's type.
        /// </summary>
        /// <value>The NPC's type.</value>
        public NpcType NpcType {
            get => _npcType;
            set {
                _npcType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's style.
        /// </summary>
        /// <value>The NPC's style.</value>
        /// <remarks>This property only affects a <see cref="NpcType.Butterfly"/>.</remarks>
        public byte Style {
            get => _style;
            set {
                _style = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _position = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            _npcType = (NpcType)reader.ReadInt16();
            _style = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((int)_position.X);
            writer.Write((int)_position.Y);
            writer.Write((short)_npcType);
            writer.Write(_style);
        }
    }
}
