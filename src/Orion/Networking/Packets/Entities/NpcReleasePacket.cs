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
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Entities;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent from the client to the server to release an NPC.
    /// </summary>
    [PublicAPI]
    public sealed class NpcReleasePacket : Packet {
        private Vector2 _npcPosition;
        private NpcType _npcType = NpcType.None;
        private byte _npcStyle;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcRelease;

        /// <summary>
        /// Gets or sets the NPC's position.
        /// </summary>
        public Vector2 NpcPosition {
            get => _npcPosition;
            set {
                _npcPosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's type.
        /// </summary>
        public NpcType NpcType {
            get => _npcType;
            set {
                _npcType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's style. This only affects butterflies.
        /// </summary>
        public byte NpcStyle {
            get => _npcStyle;
            set {
                _npcStyle = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcType}_{NpcStyle} @ {NpcPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcPosition = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            _npcType = (NpcType)reader.ReadInt16();
            _npcStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((int)_npcPosition.X);
            writer.Write((int)_npcPosition.Y);
            writer.Write((short)_npcType);
            writer.Write(_npcStyle);
        }
    }
}
