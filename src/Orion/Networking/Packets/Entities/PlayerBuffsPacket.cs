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
using Orion.Entities;
using Orion.Utils;

namespace Orion.Networking.Packets.Entities {
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
        public DirtiableArray<BuffType> PlayerBuffTypes { get; } =
            new DirtiableArray<BuffType>(Terraria.Player.maxBuffs);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerBuffsPacket"/> class.
        /// </summary>
        public PlayerBuffsPacket() {
            for (var i = 0; i < PlayerBuffTypes.Count; ++i) {
                PlayerBuffTypes[i] = BuffType.None;
            }
        }

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
                PlayerBuffTypes[i] = (BuffType)reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            foreach (var buffType in PlayerBuffTypes) {
                writer.Write((byte)buffType);
            }
        }
    }
}
