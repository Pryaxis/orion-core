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

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the client to the server to complete an angler quest.
    /// </summary>
    public struct AnglerQuestCompletePacket : IPacket
    {
        PacketId IPacket.Id => PacketId.AnglerQuestComplete;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) => 0;

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => 0;
    }
}
