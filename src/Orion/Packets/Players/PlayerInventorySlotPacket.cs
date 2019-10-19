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
using Orion.Items;
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's inventory slot.
    /// </summary>
    public sealed class PlayerInventorySlotPacket : Packet {
        private byte _playerIndex;
        private byte _inventorySlot;
        private short _stackSize;
        private ItemPrefix _itemPrefix;
        private ItemType _itemType;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerInventorySlot;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's inventory slot.
        /// </summary>
        /// <value>The player's inventory slot.</value>
        /// <remarks>
        /// This value can range from <c>0</c> to <c>219</c>. Check the <see cref="IPlayerInventory"/> interface for a
        /// more detailed description on the slots.
        /// </remarks>
        public byte InventorySlot {
            get => _inventorySlot;
            set {
                _inventorySlot = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        public short StackSize {
            get => _stackSize;
            set {
                _stackSize = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
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
        /// <value>The item's type.</value>
        public ItemType ItemType {
            get => _itemType;
            set {
                _itemType = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _inventorySlot = reader.ReadByte();
            _stackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _itemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(InventorySlot);
            writer.Write(_stackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write((short)_itemType);
        }
    }
}
