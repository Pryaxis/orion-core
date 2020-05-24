// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Text;

namespace Orion.Packets.Client {
    /// <summary>
    /// A packet sent from the client to the server to connect.
    /// </summary>
    public struct ClientConnectPacket : IPacket {
        private string? _version;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Version {
            get => _version ?? string.Empty;
            set => _version = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.ClientConnect;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => span.Read(Encoding.UTF8, out _version);

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => span.Write(Version, Encoding.UTF8);
    }
}
