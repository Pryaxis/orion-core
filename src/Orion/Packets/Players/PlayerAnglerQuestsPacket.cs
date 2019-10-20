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

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set the number of angler quests a player has completed.
    /// </summary>
    /// <remarks>This packet is not normally sent.</remarks>
    public sealed class PlayerAnglerQuestsPacket : Packet {
        private int _numberOfAnglerQuestsCompleted;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerAnglerQuests;

        /// <summary>
        /// Gets or sets the number of angler quests the player has completed.
        /// </summary>
        /// <value>The number of angler quests the player has completed.</value>
        public int NumberOfAnglerQuestsCompleted {
            get => _numberOfAnglerQuestsCompleted;
            set {
                _numberOfAnglerQuestsCompleted = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _numberOfAnglerQuestsCompleted = reader.ReadInt32();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_numberOfAnglerQuestsCompleted);
    }
}
