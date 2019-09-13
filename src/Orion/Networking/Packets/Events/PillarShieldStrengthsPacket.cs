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
    /// Packet sent from the server to the client to set the pillars' shield strengths.
    /// </summary>
    public sealed class PillarShieldStrengthsPacket : Packet {
        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort SolarPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort VortexPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort NebulaPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort StardustPillarShieldStrength { get; set; }

        public override PacketType Type => PacketType.PillarShieldStrengths;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[S={SolarPillarShieldStrength}, " +
            $"V={VortexPillarShieldStrength}, N={NebulaPillarShieldStrength}, T={StardustPillarShieldStrength}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SolarPillarShieldStrength = reader.ReadUInt16();
            VortexPillarShieldStrength = reader.ReadUInt16();
            NebulaPillarShieldStrength = reader.ReadUInt16();
            StardustPillarShieldStrength = reader.ReadUInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SolarPillarShieldStrength);
            writer.Write(VortexPillarShieldStrength);
            writer.Write(NebulaPillarShieldStrength);
            writer.Write(StardustPillarShieldStrength);
        }
    }
}
