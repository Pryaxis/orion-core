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

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's shop slot.
    /// </summary>
    public sealed class NpcShopSlotPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC shop slot index.
        /// </summary>
        public byte NpcShopSlotIndex { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemType"/>.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemPrefix"/>.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item's value.
        /// </summary>
        public int ItemValue { get; set; }

        public override PacketType Type => PacketType.NpcShopSlot;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ItemType} @ {NpcShopSlotIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcShopSlotIndex = reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemValue = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcShopSlotIndex);
            writer.Write((short)ItemType);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write(ItemValue);
        }
    }
}
