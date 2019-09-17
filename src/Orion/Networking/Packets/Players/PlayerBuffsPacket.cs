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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Entities;
using Orion.Utils;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's buffs. Each buff will be set for one second.
    /// </summary>
    public sealed class PlayerBuffsPacket : Packet {
        private readonly BuffType[] _playerBuffTypes = new BuffType[Terraria.Player.maxBuffs];
        private byte _playerIndex;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the player's buff types.
        /// </summary>
        public IArray<BuffType> PlayerBuffTypes { get; }

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerBuffs;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerBuffsPacket"/> class.
        /// </summary>
        public PlayerBuffsPacket() {
            for (var i = 0; i < _playerBuffTypes.Length; ++i) {
                _playerBuffTypes[i] = BuffType.None;
            }

            PlayerBuffTypes = new BuffTypeArray(this);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            for (var i = 0; i < _playerBuffTypes.Length; ++i) {
                _playerBuffTypes[i] = BuffType.FromId(reader.ReadByte()) ??
                                      throw new PacketException("Buff type is invalid.");
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            foreach (var buffType in PlayerBuffTypes) {
                writer.Write(buffType.Id);
            }
        }

        private class BuffTypeArray : IArray<BuffType> {
            private readonly PlayerBuffsPacket _packet;

            public BuffType this[int index] {
                get => _packet._playerBuffTypes[index];
                set {
                    _packet._playerBuffTypes[index] = value ?? throw new ArgumentNullException(nameof(value));
                    _packet.IsDirty = true;
                }
            }

            public int Count => _packet._playerBuffTypes.Length;

            public BuffTypeArray(PlayerBuffsPacket packet) {
                _packet = packet;
            }

            public IEnumerator<BuffType> GetEnumerator() =>
                ((IEnumerable<BuffType>)_packet._playerBuffTypes).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
