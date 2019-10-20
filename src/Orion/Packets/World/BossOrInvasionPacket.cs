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

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the client to the server to summon a boss or invasion.
    /// </summary>
    /// <remarks>This packet is sent whenever a player summons a boss or invasion using an item.</remarks>
    /// <seealso cref="World.BossOrInvasionType"/>
    public sealed class BossOrInvasionPacket : Packet {
        private byte _summonerIndex;
        private BossOrInvasionType _bossOrInvasionType;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.BossOrInvasion;

        /// <summary>
        /// Gets or sets the summoner's player index.
        /// </summary>
        /// <value>The summoner's player index.</value>
        public byte SummonerIndex {
            get => _summonerIndex;
            set {
                _summonerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the boss or invasion type.
        /// </summary>
        /// <value>The boss or invasion type.</value>
        public BossOrInvasionType BossOrInvasionType {
            get => _bossOrInvasionType;
            set {
                _bossOrInvasionType = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _summonerIndex = (byte)reader.ReadInt16();
            _bossOrInvasionType = (BossOrInvasionType)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)_summonerIndex);
            writer.Write((short)_bossOrInvasionType);
        }
    }
}
