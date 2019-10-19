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
    /// Packet sent from the server to the client to set the angler quest.
    /// </summary>
    public sealed class WorldAnglerQuestPacket : Packet {
        private byte _currentQuest;
        private bool _isFinished;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.WorldAnglerQuest;

        /// <summary>
        /// Gets or sets the current angler quest.
        /// </summary>
        /// <value>The current angler quest.</value>
        // TODO: implement enum for this.
        public byte CurrentQuest {
            get => _currentQuest;
            set {
                _currentQuest = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the angler quest is finished.
        /// </summary>
        /// <value><see langword="true"/> if the angler quest is finished; otherwise, <see langword="false"/>.</value>
        public bool IsFinished {
            get => _isFinished;
            set {
                _isFinished = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _currentQuest = reader.ReadByte();
            _isFinished = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_currentQuest);
            writer.Write(_isFinished);
        }
    }
}
