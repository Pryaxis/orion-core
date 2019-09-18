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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Entities;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set an NPC's shop slot.
    /// </summary>
    public sealed class NpcShopSlotPacket : Packet {
        private byte _npcShopSlotIndex;
        private ItemType _itemType = ItemType.None;
        private short _itemStackSize;
        private ItemPrefix _itemPrefix = ItemPrefix.None;
        private int _itemValue;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcShopSlot;

        /// <summary>
        /// Gets or sets the NPC shop slot index.
        /// </summary>
        public byte NpcShopSlotIndex {
            get => _npcShopSlotIndex;
            set {
                _npcShopSlotIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize {
            get => _itemStackSize;
            set {
                _itemStackSize = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's value.
        /// </summary>
        public int ItemValue {
            get => _itemValue;
            set {
                _itemValue = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{ItemType} @ {NpcShopSlotIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcShopSlotIndex = reader.ReadByte();
            ItemType = ItemType.FromId(reader.ReadInt16()) ?? throw new PacketException("Item type is invalid.");
            ItemStackSize = reader.ReadInt16();
            ItemPrefix = ItemPrefix.FromId(reader.ReadByte()) ?? throw new PacketException("Item prefix is invalid.");
            ItemValue = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcShopSlotIndex);
            writer.Write(ItemType.Id);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix.Id);
            writer.Write(ItemValue);
        }
    }
}
