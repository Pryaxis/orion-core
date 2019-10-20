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

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the server to the client to set a player's chest index.
    /// </summary>
    /// <remarks>This packet is used to synchronize player state.</remarks>
    public sealed class PlayerChestPacket : Packet {
        private byte _playerIndex;
        private short _playerChestIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerChest;

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
        /// Gets or sets the player's chest index.
        /// </summary>
        /// <value>The player's chest index.</value>
        /// <remarks>
        /// A value of <c>-1</c> indicates no chest and <c>-2</c> indicates a <see cref="ItemType.MoneyTrough"/>.
        /// </remarks>
        public short PlayerChestIndex {
            get => _playerChestIndex;
            set {
                _playerChestIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerChestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_playerChestIndex);
        }
    }
}
