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

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to teleport an NPC through a portal.
    /// </summary>
    public sealed class TeleportNpcPortalPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the portal index.
        /// </summary>
        public short PortalIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's new position.
        /// </summary>
        public Vector2 NewNpcPosition { get; set; }

        /// <summary>
        /// Gets or sets the NPC's new velocity.
        /// </summary>
        public Vector2 NewNpcVelocity { get; set; }

        internal override PacketType Type => PacketType.TeleportNpcPortal;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} @ {NewNpcPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            PortalIndex = reader.ReadInt16();
            NewNpcPosition = reader.ReadVector2();
            NewNpcVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(PortalIndex);
            writer.Write(NewNpcPosition);
            writer.Write(NewNpcVelocity);
        }
    }
}
