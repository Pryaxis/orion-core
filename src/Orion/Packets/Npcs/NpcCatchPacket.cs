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
using Orion.Items;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the client to the server to catch an NPC.
    /// </summary>
    /// <remarks>
    /// This packet is sent when a player catches an NPC using a <see cref="ItemType.BugNet"/> or
    /// <see cref="ItemType.GoldenBugNet"/>.</remarks>
    public sealed class NpcCatchPacket : Packet {
        private short _npcIndex;
        private byte _catcherIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcCatch;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC catcher's player index.
        /// </summary>
        /// <value>The NPC catcher's player index.</value>
        public byte CatcherIndex {
            get => _catcherIndex;
            set {
                _catcherIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _catcherIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_catcherIndex);
        }
    }
}
