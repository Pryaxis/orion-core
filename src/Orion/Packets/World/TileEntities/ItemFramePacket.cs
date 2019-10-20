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

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to set an item frame.
    /// </summary>
    /// <remarks>This packet is sent when a player places an item into an item frame.</remarks>
    public sealed class ItemFramePacket : Packet {
        private short _x;
        private short _y;
        private ItemType _itemType;
        private ItemPrefix _itemPrefix;
        private short _stackSize;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ItemFrame;

        /// <summary>
        /// Gets or sets the item frame's X coordinate.
        /// </summary>
        /// <value>The item frame's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item frame's Y coordinate.
        /// </summary>
        /// <value>The item frame's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
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
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        /// <remarks>
        /// This property's value is not normally greater than 1 because players can only place single items.
        /// </remarks>
        public short StackSize {
            get => _stackSize;
            set {
                _stackSize = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _itemType = (ItemType)reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _stackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_x);
            writer.Write(_y);
            writer.Write((short)_itemType);
            writer.Write((byte)_itemPrefix);
            writer.Write(_stackSize);
        }
    }
}
