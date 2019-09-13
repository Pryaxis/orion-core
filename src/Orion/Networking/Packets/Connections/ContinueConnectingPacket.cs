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
using Orion.Networking.Packets.Players;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the server to the client to allow it to continue connecting. This is sent in response to either
    /// a <see cref="PlayerConnectPacket"/> or a valid <see cref="PasswordResponsePacket"/>.
    /// </summary>
    public sealed class ContinueConnectingPacket : Packet {
        /// <summary>
        /// Gets or sets the player index that the client will begin using.
        /// </summary>
        public byte PlayerIndex { get; set; }

        internal override PacketType Type => PacketType.ContinueConnecting;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
        }
    }
}
