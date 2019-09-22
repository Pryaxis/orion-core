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
using JetBrains.Annotations;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent to either request or set an NPC's name.
    /// </summary>
    [PublicAPI]
    public sealed class NpcNamePacket : Packet {
        private short _npcIndex;
        private string _npcName = "";

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcName;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        [NotNull]
        public string NpcName {
            get => _npcName;
            set {
                _npcName = value ?? throw new ArgumentNullException(nameof(NpcName));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} is {NpcName ?? "?"}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();

            // The packet includes the NPC name if it is read as the client.
            if (context == PacketContext.Client) {
                _npcName = reader.ReadString();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);

            // The packet includes the NPC name if it is written as the server.
            if (context == PacketContext.Server) {
                writer.Write(_npcName);
            }
        }
    }
}
