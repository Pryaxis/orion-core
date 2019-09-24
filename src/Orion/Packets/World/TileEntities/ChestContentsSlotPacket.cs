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
using JetBrains.Annotations;
using Orion.Items;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set a chest contents slot. This is sent in response to a <see cref="ChestOpenPacket"/>.
    /// </summary>
    [PublicAPI]
    public sealed class ChestContentsSlotPacket : Packet {
        private short _chestIndex;
        private byte _chestContentsSlotIndex;
        private short _itemStackSize;
        private ItemPrefix _itemPrefix;
        private ItemType _itemType;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ChestContentsSlot;

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex {
            get => _chestIndex;
            set {
                _chestIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest contents slot index.
        /// </summary>
        public byte ChestContentsSlotIndex {
            get => _chestContentsSlotIndex;
            set {
                _chestContentsSlotIndex = value;
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
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ChestIndex}, {ChestContentsSlotIndex} is " +
            $"{(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : "")}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _chestIndex = reader.ReadInt16();
            _chestContentsSlotIndex = reader.ReadByte();
            _itemStackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _itemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_chestIndex);
            writer.Write(_chestContentsSlotIndex);
            writer.Write(_itemStackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write((short)_itemType);
        }
    }
}
