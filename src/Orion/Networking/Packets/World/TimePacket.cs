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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to set the time.
    /// </summary>
    public sealed class TimePacket : Packet {
        /// <summary>
        /// Gets or sets a value indicating whether it is daytime.
        /// </summary>
        public bool IsDaytime { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the sun's Y position.
        /// </summary>
        public short SunY { get; set; }

        /// <summary>
        /// Gets or sets the moon's Y position.
        /// </summary>
        public short MoonY { get; set; }

        internal override PacketType Type => PacketType.Time;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Time} @ {(IsDaytime ? "day" : "night")}, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            IsDaytime = reader.ReadByte() == 1;
            Time = reader.ReadInt32();
            SunY = reader.ReadInt16();
            MoonY = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(IsDaytime);
            writer.Write(Time);
            writer.Write(SunY);
            writer.Write(MoonY);
        }
    }
}
