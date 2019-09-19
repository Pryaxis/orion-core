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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to show a player dodge.
    /// </summary>
    public sealed class PlayerDodgePacket : Packet {
        private byte _playerIndex;
        private DodgeType _playerDodgeType;

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
        /// Gets or sets the player's dodge type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public DodgeType PlayerDodgeType {
            get => _playerDodgeType;
            set {
                _playerDodgeType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerDodge;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} {PlayerDodgeType}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerDodgeType = DodgeType.FromId(reader.ReadByte()) ??
                              throw new PacketException("Dodge type is invalid.");
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerDodgeType.Id);
        }

        /// <summary>
        /// Represents a dodge type in a <see cref="PlayerDodgePacket"/>.
        /// </summary>
        public sealed class DodgeType {
#pragma warning disable 1591
            public static readonly DodgeType NinjaDodge = new DodgeType(1);
            public static readonly DodgeType ShadowDodge = new DodgeType(2);
#pragma warning restore 1591

            private const int ArrayOffset = 0;
            private const int ArraySize = ArrayOffset + 3;
            private static readonly DodgeType[] Dodges = new DodgeType[ArraySize];
            private static readonly string[] Names = new string[ArraySize];

            /// <summary>
            /// Gets the dodge type's ID.
            /// </summary>
            public byte Id { get; }

            static DodgeType() {
                foreach (var field in typeof(DodgeType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                    var dodgeType = (DodgeType)field.GetValue(null);
                    Dodges[ArrayOffset + dodgeType.Id] = dodgeType;
                    Names[ArrayOffset + dodgeType.Id] = field.Name;
                }
            }

            private DodgeType(byte id) {
                Id = id;
            }

            /// <summary>
            /// Returns a dodge type converted from the given ID.
            /// </summary>
            /// <param name="id">The ID.</param>
            /// <returns>The dodge type, or <c>null</c> if none exists.</returns>
            public static DodgeType FromId(byte id) => ArrayOffset + id < ArraySize ? Dodges[ArrayOffset + id] : null;

            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public override string ToString() => Names[ArrayOffset + Id];
        }
    }
}
