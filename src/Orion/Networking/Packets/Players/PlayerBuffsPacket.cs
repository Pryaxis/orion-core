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
using Orion.Events;
using Orion.Utils;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's buffs. Each buff will be set for one second.
    /// </summary>
    public sealed class PlayerBuffsPacket : Packet {
        private byte _playerIndex;

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || PlayerBuffTypes.IsDirty;

        /// <inheritdoc />
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
        public BuffTypes PlayerBuffTypes { get; } = new BuffTypes();

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            PlayerBuffTypes.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            for (var i = 0; i < PlayerBuffTypes.Count; ++i) {
                PlayerBuffTypes[i] = BuffType.FromId(reader.ReadByte()) ??
                                     throw new PacketException("Buff type is invalid.");
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            foreach (var buffType in PlayerBuffTypes) {
                writer.Write(buffType.Id);
            }
        }

        /// <summary>
        /// Represents the buff types in a <see cref="PlayerBuffsPacket"/>.
        /// </summary>
        public sealed class BuffTypes : IArray<BuffType>, IDirtiable {
            private readonly BuffType[] _buffTypes = new BuffType[Terraria.Player.maxBuffs];

            /// <inheritdoc cref="IArray{T}.this" />
            public BuffType this[int index] {
                get => _buffTypes[index];
                set {
                    _buffTypes[index] = value ?? throw new ArgumentNullException(nameof(value));
                    IsDirty = true;
                }
            }

            /// <inheritdoc />
            public int Count => _buffTypes.Length;

            /// <inheritdoc />
            public bool IsDirty { get; private set; }

            internal BuffTypes() {
                for (var i = 0; i < _buffTypes.Length; ++i) {
                    _buffTypes[i] = BuffType.None;
                }
            }

            /// <inheritdoc />
            public IEnumerator<BuffType> GetEnumerator() => ((IEnumerable<BuffType>)_buffTypes).GetEnumerator();

            [ExcludeFromCodeCoverage]
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <inheritdoc />
            public void Clean() {
                IsDirty = false;
            }
        }
    }
}
