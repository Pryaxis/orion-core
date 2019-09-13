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
using Orion.Npcs;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the client to the server to release an NPC.
    /// </summary>
    public sealed class ReleaseNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC's position.
        /// </summary>
        public Vector2 NpcPosition { get; set; }

        /// <summary>
        /// Gets or sets the NPC's <see cref="Orion.Npcs.NpcType"/>.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the NPC's style.
        /// </summary>
        public byte NpcStyle { get; set; }

        internal override PacketType Type => PacketType.ReleaseNpc;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{NpcType}_{NpcStyle} @ {NpcPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcPosition = new Vector2(reader.ReadInt32(), reader.ReadInt32());
            NpcType = (NpcType)reader.ReadInt16();
            NpcStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((int)NpcPosition.X);
            writer.Write((int)NpcPosition.Y);
            writer.Write((short)NpcType);
            writer.Write(NpcStyle);
        }
    }
}
