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
    /// Packet sent to play an instrument note from a player.
    /// </summary>
    /// <remarks>
    /// This packet is sent when a player plays a <see cref="ItemType.Harp"/>, <see cref="ItemType.MagicalHarp"/>,
    /// or <see cref="ItemType.Bell"/>.
    /// </remarks>
    public sealed class PlayerInstrumentNotePacket : Packet {
        private byte _playerIndex;
        private float _instrumentNote;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerInstrumentNote;

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
        /// Gets or sets the player's instrument note.
        /// </summary>
        /// <value>The player's instrument note.</value>
        /// <remarks>
        /// This property's value can range from <c>-1.0</c> to <c>1.0</c>. A value of <c>-1.0</c> results in middle C
        /// while a value of <c>1.0</c> results in C6.
        /// </remarks>
        public float InstrumentNote {
            get => _instrumentNote;
            set {
                _instrumentNote = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _instrumentNote = reader.ReadSingle();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_instrumentNote);
        }
    }
}
