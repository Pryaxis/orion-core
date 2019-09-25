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
using Orion.Packets.World.Tiles;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to consume a player's items. This is sent in response to a
    /// <see cref="WireMassOperationPacket"/>.
    /// </summary>
    public sealed class PlayerConsumeItemsPacket : Packet {
        private ItemType _itemType;
        private short _itemStackSize;
        private byte _playerIndex;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerConsumeItems;

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
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, {ItemType} x{ItemStackSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _itemType = (ItemType)reader.ReadInt16();
            _itemStackSize = reader.ReadInt16();
            _playerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)_itemType);
            writer.Write(_itemStackSize);
            writer.Write(_playerIndex);
        }
    }
}
