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

namespace Orion.Core.Packets
{
    /// <summary>
    /// Describes the context with which a packet should be processed.
    /// </summary>
    /// <remarks>
    /// Some packets may be processed differently depending on whether the server or the client is reading or writing
    /// the packet. This enumeration clarifies the correct action to take when reading or writing such a packet.
    /// </remarks>
    public enum PacketContext
    {
        /// <summary>
        /// Indicates that the packet should be processed as the server.
        /// </summary>
        Server = 0,

        /// <summary>
        /// Indicates that the packet should be processed as the client.
        /// </summary>
        Client = 1
    }
}
