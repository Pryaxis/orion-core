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
using JetBrains.Annotations;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent to summon a boss or an invasion. See <see cref="World.NetworkBossOrInvasion"/> for the
    /// list of bosses or invasions.
    /// </summary>
    [PublicAPI]
    public sealed class BossOrInvasionPacket : Packet {
        private byte _summmonOnPlayerIndex;
        private NetworkBossOrInvasion _networkBossOrInvasion;

        /// <inheritdoc />
        public override PacketType Type => PacketType.BossOrInvasion;

        /// <summary>
        /// Gets or sets the player index to summon the boss on.
        /// </summary>
        public byte SummmonOnPlayerIndex {
            get => _summmonOnPlayerIndex;
            set {
                _summmonOnPlayerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the network boss or invasion.
        /// </summary>
        public NetworkBossOrInvasion NetworkBossOrInvasion {
            get => _networkBossOrInvasion;
            set {
                _networkBossOrInvasion = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={SummmonOnPlayerIndex}, {NetworkBossOrInvasion}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _summmonOnPlayerIndex = (byte)reader.ReadInt16();
            _networkBossOrInvasion = (NetworkBossOrInvasion)reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)_summmonOnPlayerIndex);
            writer.Write((short)_networkBossOrInvasion);
        }
    }
}
