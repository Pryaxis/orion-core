// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Packets {
    /// <summary>
    /// Specifies the context with which to process a packet.
    /// </summary>
    public enum PacketContext {
        /// <summary>
        /// Indicates that the packet should be processed as the server. If reading a packet, then the packet should be
        /// read as if it came from the client. If writing a packet, then the packet should be written as if it came
        /// from the server.
        /// </summary>
        Server,

        /// <summary>
        /// Indicates that the packet should be processed as the client. If reading a packet, then the packet should be
        /// read as if it came from the server. If writing a packet, then the packet should be written as if it came
        /// from the client.
        /// </summary>
        Client
    }
}
