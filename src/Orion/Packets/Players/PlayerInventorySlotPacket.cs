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

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Items;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's inventory slot.
    /// </summary>
    public sealed class PlayerInventorySlotPacket : Packet {
        private byte _playerIndex;
        private byte _playerInventorySlotIndex;
        private short _itemStackSize;
        private ItemPrefix _itemPrefix;
        private ItemType _itemType;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerInventorySlot;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's inventory slot index.
        /// </summary>
        public byte PlayerInventorySlotIndex {
            get => _playerInventorySlotIndex;
            set {
                _playerInventorySlotIndex = value;
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

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={PlayerIndex}, {PlayerInventorySlotIndex} is " +
            $"{(ItemPrefix.Equals(ItemPrefix.None) ? ItemPrefix + " " : string.Empty)}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerInventorySlotIndex = reader.ReadByte();
            _itemStackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _itemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerInventorySlotIndex);
            writer.Write(_itemStackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write((short)_itemType);
        }
    }
}
