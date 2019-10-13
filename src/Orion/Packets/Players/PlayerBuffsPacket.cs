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
using Orion.Entities;
using Orion.Utils;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's buffs. Each buff will be set for one second.
    /// </summary>
    public sealed class PlayerBuffsPacket : Packet {
        private readonly DirtiableArray<BuffType> _playerBuffTypes =
            new DirtiableArray<BuffType>(TerrariaPlayer.maxBuffs);

        private byte _playerIndex;

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _playerBuffTypes.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerBuffs;

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

        /// <summary>
        /// Gets the player's buff types.
        /// </summary>
        public IArray<BuffType> PlayerBuffTypes => _playerBuffTypes;

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _playerBuffTypes.Clean();
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            for (var i = 0; i < _playerBuffTypes.Count; ++i) {
                _playerBuffTypes._array[i] = (BuffType)reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            foreach (var buffType in _playerBuffTypes) {
                writer.Write((byte)buffType);
            }
        }
    }
}
