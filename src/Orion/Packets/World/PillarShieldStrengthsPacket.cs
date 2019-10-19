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
    /// Packet sent from the server to the client to set the pillars' shield strengths.
    /// </summary>
    public sealed class PillarShieldStrengthsPacket : Packet {
        private ushort _solar;
        private ushort _vortex;
        private ushort _nebula;
        private ushort _stardust;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PillarShieldStrengths;

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        /// <value>The solar pillar's shield strength.</value>
        public ushort Solar {
            get => _solar;
            set {
                _solar = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the vortex pillar's shield strength.
        /// </summary>
        /// <value>The vortex pillar's shield strength.</value>
        public ushort Vortex {
            get => _vortex;
            set {
                _vortex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the nebula pillar's shield strength.
        /// </summary>
        /// <value>The nebula pillar's shield strength.</value>
        public ushort Nebula {
            get => _nebula;
            set {
                _nebula = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the stardust pillar's shield strength.
        /// </summary>
        /// <value>The stardust pillar's shield strength.</value>
        public ushort Stardust {
            get => _stardust;
            set {
                _stardust = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _solar = reader.ReadUInt16();
            _vortex = reader.ReadUInt16();
            _nebula = reader.ReadUInt16();
            _stardust = reader.ReadUInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_solar);
            writer.Write(_vortex);
            writer.Write(_nebula);
            writer.Write(_stardust);
        }
    }
}
