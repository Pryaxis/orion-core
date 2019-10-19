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

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's item animation.
    /// </summary>
    public sealed class PlayerItemAnimationPacket : Packet {
        private byte _playerIndex;
        private float _itemRotation;
        private short _itemAnimation;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerItemAnimation;

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

        // TODO: explain these

        /// <summary>
        /// Gets or sets the player's item rotation.
        /// </summary>
        /// <value>The player's item rotation.</value>
        public float ItemRotation {
            get => _itemRotation;
            set {
                _itemRotation = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's item animation.
        /// </summary>
        /// <value>The player's item animation.</value>
        public short ItemAnimation {
            get => _itemAnimation;
            set {
                _itemAnimation = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _itemRotation = reader.ReadSingle();
            _itemAnimation = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_itemRotation);
            writer.Write(_itemAnimation);
        }
    }
}
