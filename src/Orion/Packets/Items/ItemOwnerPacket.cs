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

using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Packets.Items {
    /// <summary>
    /// Packet sent to set an item's owner: i.e., the player who is picking up the item.
    /// </summary>
    public sealed class ItemOwnerPacket : Packet {
        private short _itemIndex;
        private byte _itemOwnerPlayerIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ItemOwner;

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
        /// Gets or sets the item owner's player index.
        /// </summary>
        public byte ItemOwnerPlayerIndex {
            get => _itemOwnerPlayerIndex;
            set {
                _itemOwnerPlayerIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ItemIndex}, P={ItemOwnerPlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _itemIndex = reader.ReadInt16();
            _itemOwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_itemIndex);
            writer.Write(_itemOwnerPlayerIndex);
        }
    }
}
