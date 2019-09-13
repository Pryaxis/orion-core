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
using Orion.Items;
using Orion.Networking.Packets.World;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to consume a player's items. This is sent in response to a
    /// <see cref="RequestMassWireOperationPacket"/>.
    /// </summary>
    public sealed class ConsumeItemsPacket : Packet {
        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemType"/>.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        public override PacketType Type => PacketType.ConsumeItems;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemType = (ItemType)reader.ReadInt16();
            ItemStackSize = reader.ReadInt16();
            PlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)ItemType);
            writer.Write(ItemStackSize);
            writer.Write(PlayerIndex);
        }
    }
}
