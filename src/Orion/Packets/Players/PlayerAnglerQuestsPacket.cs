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
using System.Diagnostics.Contracts;
using System.IO;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set the number of angler quests a player has completed. This is currently not naturally sent.
    /// </summary>
    public sealed class PlayerAnglerQuestsPacket : Packet {
        private int _playerNumberOfAnglerQuestsCompleted;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerAnglerQuests;

        /// <summary>
        /// Gets or sets the player's number of angler quests completed.
        /// </summary>
        public int PlayerNumberOfAnglerQuestsCompleted {
            get => _playerNumberOfAnglerQuestsCompleted;
            set {
                _playerNumberOfAnglerQuestsCompleted = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{PlayerNumberOfAnglerQuestsCompleted}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _playerNumberOfAnglerQuestsCompleted = reader.ReadInt32();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_playerNumberOfAnglerQuestsCompleted);
    }
}
