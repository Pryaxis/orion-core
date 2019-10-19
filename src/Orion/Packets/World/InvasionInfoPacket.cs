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
    /// Packet sent from the server to the client to set invasion information.
    /// </summary>
    public sealed class InvasionInfoPacket : Packet {
        private int _numberOfKills;
        private int _numberOfKillsToProgress;
        private sbyte _iconType;
        private sbyte _waveNumber;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.InvasionInfo;

        /// <summary>
        /// Gets or sets the number of kills for the current wave.
        /// </summary>
        /// <value>The number of kills for the current wave.</value>
        public int NumberOfKills {
            get => _numberOfKills;
            set {
                _numberOfKills = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number of kills to progress the the next wave.
        /// </summary>
        /// <value>The number of kills to progress to the next wave.</value>
        public int NumberOfKillsToProgress {
            get => _numberOfKillsToProgress;
            set {
                _numberOfKillsToProgress = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the invasion's icon type.
        /// </summary>
        /// <value>The invasion's icon type.</value>
        // TODO: implement enum for this.
        public sbyte IconType {
            get => _iconType;
            set {
                _iconType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the invasion's wave number.
        /// </summary>
        /// <value>The invasion's wave number.</value>
        public sbyte WaveNumber {
            get => _waveNumber;
            set {
                _waveNumber = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _numberOfKills = reader.ReadInt32();
            _numberOfKillsToProgress = reader.ReadInt32();
            _iconType = reader.ReadSByte();
            _waveNumber = reader.ReadSByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_numberOfKills);
            writer.Write(_numberOfKillsToProgress);
            writer.Write(_iconType);
            writer.Write(_waveNumber);
        }
    }
}
