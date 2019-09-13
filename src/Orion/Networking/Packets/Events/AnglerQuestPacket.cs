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

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the angler quest.
    /// </summary>
    public sealed class AnglerQuestPacket : Packet {
        /// <summary>
        /// Gets or sets the angler quest.
        /// </summary>

        // TODO: implement enum for this.
        public byte CurrentAnglerQuest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the angler quest is finished.
        /// </summary>
        public bool IsAnglerQuestFinished { get; set; }

        public override PacketType Type => PacketType.AnglerQuest;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[{CurrentAnglerQuest}, F={IsAnglerQuestFinished}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            CurrentAnglerQuest = reader.ReadByte();
            IsAnglerQuestFinished = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(CurrentAnglerQuest);
            writer.Write(IsAnglerQuestFinished);
        }
    }
}
