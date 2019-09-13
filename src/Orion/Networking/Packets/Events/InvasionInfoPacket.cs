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

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set invasion information.
    /// </summary>
    public sealed class InvasionInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the number of kills in the current wave.
        /// </summary>
        public int NumberOfKills { get; set; }

        /// <summary>
        /// Gets or sets the number of kills to progress the current wave.
        /// </summary>
        public int NumberOfKillsToProgress { get; set; }

        /// <summary>
        /// Gets or sets the invasion icon type.
        /// </summary>

        // TODO: implement enum for this.
        public int InvasionIconType { get; set; }

        /// <summary>
        /// Gets or sets the wave number.
        /// </summary>
        public int InvasionWaveNumber { get; set; }

        public override PacketType Type => PacketType.InvasionInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={InvasionWaveNumber}: {NumberOfKills}/{NumberOfKillsToProgress}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NumberOfKills = reader.ReadInt32();
            NumberOfKillsToProgress = reader.ReadInt32();
            InvasionIconType = reader.ReadInt32();
            InvasionWaveNumber = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NumberOfKills);
            writer.Write(NumberOfKillsToProgress);
            writer.Write(InvasionIconType);
            writer.Write(InvasionWaveNumber);
        }
    }
}
