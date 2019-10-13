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

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to set an item frame.
    /// </summary>
    public sealed class ItemFramePacket : Packet {
        private short _itemFrameX;
        private short _itemFrameY;
        private ItemType _itemType;
        private ItemPrefix _itemPrefix;
        private short _itemStackSize;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ItemFrame;

        /// <summary>
        /// Gets or sets the item frame's X coordinate.
        /// </summary>
        public short ItemFrameX {
            get => _itemFrameX;
            set {
                _itemFrameX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item frame's Y coordinate.
        /// </summary>
        public short ItemFrameY {
            get => _itemFrameY;
            set {
                _itemFrameY = value;
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
        /// Gets or sets the item's stack size.
        /// </summary>
        public short ItemStackSize {
            get => _itemStackSize;
            set {
                _itemStackSize = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[({ItemFrameX}, {ItemFrameY}) is " +
            $"{(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : string.Empty)}{ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _itemFrameX = reader.ReadInt16();
            _itemFrameY = reader.ReadInt16();
            _itemType = (ItemType)reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _itemStackSize = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_itemFrameX);
            writer.Write(_itemFrameY);
            writer.Write((short)_itemType);
            writer.Write((byte)_itemPrefix);
            writer.Write(_itemStackSize);
        }
    }
}
