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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set invasion information.
    /// </summary>
    [PublicAPI]
    public sealed class InvasionInfoPacket : Packet {
        private int _numberOfKills;
        private int _numberOfKillsToProgress;
        private int _invasionIconType;
        private int _invasionWaveNumber;

        /// <inheritdoc />
        public override PacketType Type => PacketType.InvasionInfo;

        /// <summary>
        /// Gets or sets the number of kills in the current wave.
        /// </summary>
        public int NumberOfKills {
            get => _numberOfKills;
            set {
                _numberOfKills = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number of kills to progress the current wave.
        /// </summary>
        public int NumberOfKillsToProgress {
            get => _numberOfKillsToProgress;
            set {
                _numberOfKillsToProgress = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the invasion icon type.
        /// </summary>

        // TODO: implement enum for this.
        public int InvasionIconType {
            get => _invasionIconType;
            set {
                _invasionIconType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the wave number.
        /// </summary>
        public int InvasionWaveNumber {
            get => _invasionWaveNumber;
            set {
                _invasionWaveNumber = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={InvasionWaveNumber}: {NumberOfKills}/{NumberOfKillsToProgress}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _numberOfKills = reader.ReadInt32();
            _numberOfKillsToProgress = reader.ReadInt32();
            _invasionIconType = reader.ReadInt32();
            _invasionWaveNumber = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_numberOfKills);
            writer.Write(_numberOfKillsToProgress);
            writer.Write(_invasionIconType);
            writer.Write(_invasionWaveNumber);
        }
    }
}
