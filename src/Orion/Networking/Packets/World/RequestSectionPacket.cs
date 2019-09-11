﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the client to the server to request a section of the world.
    /// </summary>
    public sealed class RequestSectionPacket : Packet {
        /// <summary>
        /// Gets or sets the section's X index. An invalid value results in only the spawn section being sent.
        /// </summary>
        public int SectionX { get; set; }

        /// <summary>
        /// Gets or sets the section's Y index. An invalid value results in only the spawn section being sent.
        /// </summary>
        public int SectionY { get; set; }

        private protected override PacketType Type => PacketType.RequestSection;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({SectionX}, {SectionY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SectionX = reader.ReadInt32();
            SectionY = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SectionX);
            writer.Write(SectionY);
        }
    }
}