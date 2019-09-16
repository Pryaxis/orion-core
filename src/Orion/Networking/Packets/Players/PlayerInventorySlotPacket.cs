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

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's inventory slot.
    /// </summary>
    public sealed class PlayerInventorySlotPacket : Packet {
        private byte _playerIndex;
        private byte _playerInventorySlotIndex;
        private short _itemStackSize;
        private ItemPrefix _itemPrefix = ItemPrefix.None;
        private ItemType _itemType = ItemType.None;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's inventory slot index.
        /// </summary>
        public byte PlayerInventorySlotIndex {
            get => _playerInventorySlotIndex;
            set {
                _playerInventorySlotIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize {
            get => _itemStackSize;
            set {
                _itemStackSize = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's item prefix.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ItemPrefix ItemPrefix {
            get => _itemPrefix;
            set {
                _itemPrefix = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's item type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerInventorySlot;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={PlayerIndex}, {PlayerInventorySlotIndex} is " +
            $"{(ItemPrefix.Equals(ItemPrefix.None) ? ItemPrefix + " " : "")}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerInventorySlotIndex = reader.ReadByte();
            _itemStackSize = reader.ReadInt16();
            _itemPrefix = ItemPrefix.FromId(reader.ReadByte());
            _itemType = ItemType.FromId(reader.ReadInt16());
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerInventorySlotIndex);
            writer.Write(ItemStackSize);
            writer.Write((byte)ItemPrefix.Id);
            writer.Write(ItemType.Id);
        }
    }
}
