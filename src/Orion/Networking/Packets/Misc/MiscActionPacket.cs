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

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to perform a miscellaneous action.
    /// </summary>
    public sealed class MiscActionPacket : Packet {
        /// <summary>
        /// Gets or sets the player or NPC index.
        /// </summary>
        public byte PlayerOrNpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public MiscAction Action { get; set; }

        internal override PacketType Type => PacketType.MiscAction;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Action} by #={PlayerOrNpcIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerOrNpcIndex = reader.ReadByte();
            Action = (MiscAction)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerOrNpcIndex);
            writer.Write((byte)Action);
        }
    }
}
