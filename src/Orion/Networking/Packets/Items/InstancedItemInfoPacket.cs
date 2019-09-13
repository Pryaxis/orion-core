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
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Items {
    /// <summary>
    /// Packet sent to set instanced item information.
    /// </summary>
    public sealed class InstancedItemInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the item's position.
        /// </summary>
        public Vector2 ItemPosition { get; set; }

        /// <summary>
        /// Gets or sets the item's velocity.
        /// </summary>
        public Vector2 ItemVelocity { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemPrefix"/>.
        /// </summary>
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item should be disowned.
        /// </summary>
        public bool ShouldDisownItem { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Orion.Items.ItemType"/>.
        /// </summary>
        public ItemType ItemType { get; set; }

        internal override PacketType Type => PacketType.InstancedItemInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ItemIndex}, {(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : "")}{ItemType} " +
            $"x{ItemStackSize} @ {ItemPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemIndex = reader.ReadInt16();
            ItemPosition = reader.ReadVector2();
            ItemVelocity = reader.ReadVector2();
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = (ItemPrefix)reader.ReadByte();
            ShouldDisownItem = reader.ReadBoolean();
            ItemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemIndex);
            writer.Write(ItemPosition);
            writer.Write(ItemVelocity);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix);
            writer.Write(ShouldDisownItem);
            writer.Write((short)ItemType);
        }
    }
}
