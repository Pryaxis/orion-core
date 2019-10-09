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

using System.IO;
using Orion.Packets.World.Tiles;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to enter the world. This is sent in response to a
    /// <see cref="SectionRequestPacket"/>.
    /// </summary>
    public sealed class PlayerEnterWorldPacket : Packet {
        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerEnterWorld;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) { }
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) { }
    }
}
