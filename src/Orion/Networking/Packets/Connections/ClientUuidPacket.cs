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

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the client to the server to inform the server about the client's UUID. This is sent in response
    /// to a <see cref="ContinueConnectingPacket"/>.
    /// </summary>
    public sealed class ClientUuidPacket : Packet {
        private string _clientUuid;

        /// <summary>
        /// Gets or sets the client's UUID.
        /// </summary>
        public string ClientUuid {
            get => _clientUuid;
            set => _clientUuid = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override PacketType Type => PacketType.ClientUuid;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ClientUuid}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _clientUuid = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ClientUuid);
        }
    }
}
