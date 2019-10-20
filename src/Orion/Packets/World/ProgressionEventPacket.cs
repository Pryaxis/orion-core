﻿// Copyright (c) 2019 Pryaxis & Orion Contributors
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
    /// Packet sent from the server to the client to notify a progression event. This is used for achievements.
    /// </summary>
    /// <remarks>This packet is used for achievement tracking.</remarks>
    public sealed class ProgressionEventPacket : Packet {
        private short _eventId;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ProgressionEvent;

        /// <summary>
        /// Gets or sets the progression event's ID.
        /// </summary>
        /// <value>The progression event's ID.</value>
        // TODO: implement enum for this.
        public short EventId {
            get => _eventId;
            set {
                _eventId = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _eventId = reader.ReadInt16();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_eventId);
    }
}
