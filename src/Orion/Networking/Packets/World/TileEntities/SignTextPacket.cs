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

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set a sign's text. This is sent in response to a <see cref="RequestSignPacket"/>.
    /// </summary>
    public sealed class SignTextPacket : Packet {
        private string _signText;

        /// <summary>
        /// Gets or sets the sign's index.
        /// </summary>
        public short SignIndex { get; set; }

        /// <summary>
        /// Gets or sets the sign's X coordinate.
        /// </summary>
        public short SignX { get; set; }

        /// <summary>
        /// Gets or sets the sign's Y coordinate.
        /// </summary>
        public short SignY { get; set; }

        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string SignText {
            get => _signText;
            set => _signText = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.SignText;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={SignIndex} @ ({SignX}, {SignY}): \"{SignText}\"]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SignIndex = reader.ReadInt16();
            SignX = reader.ReadInt16();
            SignY = reader.ReadInt16();
            _signText = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SignIndex);
            writer.Write(SignX);
            writer.Write(SignY);
            writer.Write(SignText);
        }
    }
}
