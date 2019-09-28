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
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Packets.Extensions;

namespace Orion.Packets.Items {
    /// <summary>
    /// Packet sent to set item instance information. This is similar to an <see cref="ItemInfoPacket"/>, but instanced
    /// items are not visible to other players.
    /// </summary>
    public sealed class ItemInstanceInfoPacket : Packet {
        private short _itemIndex;
        private Vector2 _itemPosition;
        private Vector2 _itemVelocity;
        private short _itemStackSize;
        private ItemPrefix _itemPrefix;
        private bool _canBePickedUpImmediately;
        private ItemType _itemType;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ItemInstanceInfo;

        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex {
            get => _itemIndex;
            set {
                _itemIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's position.
        /// </summary>
        public Vector2 ItemPosition {
            get => _itemPosition;
            set {
                _itemPosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the item's velocity.
        /// </summary>
        public Vector2 ItemVelocity {
            get => _itemVelocity;
            set {
                _itemVelocity = value;
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
        /// Gets or sets a value indicating whether the item can be picked up immediately.
        /// </summary>
        public bool CanBePickedUpImmediately {
            get => _canBePickedUpImmediately;
            set {
                _canBePickedUpImmediately = value;
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
            $"{Type}[#={ItemIndex}, {(ItemPrefix != ItemPrefix.None ? ItemPrefix + " " : "")}{ItemType} " +
            $"x{ItemStackSize} @ {ItemPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _itemIndex = reader.ReadInt16();
            _itemPosition = reader.ReadVector2();
            _itemVelocity = reader.ReadVector2();
            _itemStackSize = reader.ReadInt16();
            _itemPrefix = (ItemPrefix)reader.ReadByte();
            _canBePickedUpImmediately = reader.ReadBoolean();
            _itemType = (ItemType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_itemIndex);
            writer.Write(_itemPosition);
            writer.Write(_itemVelocity);
            writer.Write(_itemStackSize);
            writer.Write((byte)_itemPrefix);
            writer.Write(_canBePickedUpImmediately);
            writer.Write((short)_itemType);
        }
    }
}
