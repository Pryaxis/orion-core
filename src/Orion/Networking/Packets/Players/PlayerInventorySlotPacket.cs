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

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's inventory slot.
    /// </summary>
    public sealed class PlayerInventorySlotPacket : Packet {
        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's inventory slot index.
        /// </summary>
        public byte PlayerInventorySlotIndex { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemPrefix"/>.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemType"/>.
        /// </summary>
        public ItemType ItemType { get; set; }

        public override PacketType Type => PacketType.PlayerInventorySlot;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={PlayerIndex}, {PlayerInventorySlotIndex} is " +
            $"{(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : "")}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerInventorySlotIndex = reader.ReadByte();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerInventorySlotIndex);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write((short)ItemType);
        }
    }
}
