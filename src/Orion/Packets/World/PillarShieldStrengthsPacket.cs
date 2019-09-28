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
using System.IO;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set the pillars' shield strengths.
    /// </summary>
    public sealed class PillarShieldStrengthsPacket : Packet {
        private ushort _solarPillarShieldStrength;
        private ushort _vortexPillarShieldStrength;
        private ushort _nebulaPillarShieldStrength;
        private ushort _stardustPillarShieldStrength;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PillarShieldStrengths;

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort SolarPillarShieldStrength {
            get => _solarPillarShieldStrength;
            set {
                _solarPillarShieldStrength = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort VortexPillarShieldStrength {
            get => _vortexPillarShieldStrength;
            set {
                _vortexPillarShieldStrength = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort NebulaPillarShieldStrength {
            get => _nebulaPillarShieldStrength;
            set {
                _nebulaPillarShieldStrength = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort StardustPillarShieldStrength {
            get => _stardustPillarShieldStrength;
            set {
                _stardustPillarShieldStrength = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[S={SolarPillarShieldStrength}, " +
            $"V={VortexPillarShieldStrength}, N={NebulaPillarShieldStrength}, T={StardustPillarShieldStrength}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _solarPillarShieldStrength = reader.ReadUInt16();
            _vortexPillarShieldStrength = reader.ReadUInt16();
            _nebulaPillarShieldStrength = reader.ReadUInt16();
            _stardustPillarShieldStrength = reader.ReadUInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_solarPillarShieldStrength);
            writer.Write(_vortexPillarShieldStrength);
            writer.Write(_nebulaPillarShieldStrength);
            writer.Write(_stardustPillarShieldStrength);
        }
    }
}
