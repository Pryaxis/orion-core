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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set the biome stats.
    /// </summary>
    public sealed class BiomeStatsPacket : Packet {
        private byte _hallowedAmount;
        private byte _corruptionAmount;
        private byte _crimsonAmount;

        /// <inheritdoc />
        public override PacketType Type => PacketType.BiomeStats;

        /// <summary>
        /// Gets or sets the hallowed biome amount.
        /// </summary>
        public byte HallowedAmount {
            get => _hallowedAmount;
            set {
                _hallowedAmount = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the corruption biome amount.
        /// </summary>
        public byte CorruptionAmount {
            get => _corruptionAmount;
            set {
                _corruptionAmount = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the crimson biome amount.
        /// </summary>
        public byte CrimsonAmount {
            get => _crimsonAmount;
            set {
                _crimsonAmount = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[H={HallowedAmount} vs. (C={CorruptionAmount} or C'={CrimsonAmount})]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            HallowedAmount = reader.ReadByte();
            CorruptionAmount = reader.ReadByte();
            CrimsonAmount = reader.ReadByte();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(HallowedAmount);
            writer.Write(CorruptionAmount);
            writer.Write(CrimsonAmount);
        }
    }
}
