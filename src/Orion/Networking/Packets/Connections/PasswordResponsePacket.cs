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
    /// Packet sent from the client to the server to try a password. This is sent in response to a
    /// <see cref="RequestPasswordPacket"/>.
    /// </summary>
    public sealed class PasswordResponsePacket : Packet {
        private string _password = "";

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Password {
            get => _password;
            set => _password = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.PasswordResponse;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Password}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _password = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(Password);
        }
    }
}
