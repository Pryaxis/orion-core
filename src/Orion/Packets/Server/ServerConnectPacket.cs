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

namespace Orion.Packets.Server {
    /// <summary>
    /// Packet sent from the client to the server to connect.
    /// </summary>
    public struct ServerConnectPacket : IPacket {
        /// <summary>
        /// The client's version.
        /// </summary>
        public string? Version;

        /// <inheritdoc/>
        public void Read(ReadOnlySpan<byte> span, PacketContext context) {
            Version = SpanUtils.ReadString(ref span, Encoding.UTF8);
        }

        /// <inheritdoc/>
        public void Write(ref Span<byte> span, PacketContext context) {
            SpanUtils.Write(ref span, Version ?? "", Encoding.UTF8);
        }
    }
}
