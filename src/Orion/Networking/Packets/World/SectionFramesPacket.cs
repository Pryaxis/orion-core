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

using System.IO;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to synchronize tile frames. This is sent following every
    /// <see cref="SectionPacket"/>.
    /// </summary>
    public sealed class SectionFramesPacket : Packet {
        /// <summary>
        /// Gets or sets the starting section's X index.
        /// </summary>
        public short StartSectionX { get; set; }

        /// <summary>
        /// Gets or sets the starting section's Y index.
        /// </summary>
        public short StartSectionY { get; set; }

        /// <summary>
        /// Gets or sets the ending section's X index.
        /// </summary>
        public short EndSectionX { get; set; }

        /// <summary>
        /// Gets or sets the ending section's Y index.
        /// </summary>
        public short EndSectionY { get; set; }

        public override PacketType Type => PacketType.SectionFrames;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            StartSectionX = reader.ReadInt16();
            StartSectionY = reader.ReadInt16();
            EndSectionX = reader.ReadInt16();
            EndSectionY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(StartSectionX);
            writer.Write(StartSectionY);
            writer.Write(EndSectionX);
            writer.Write(EndSectionY);
        }
    }
}
