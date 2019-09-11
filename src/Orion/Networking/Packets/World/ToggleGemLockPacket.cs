﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to toggle a gem lock.
    /// </summary>
    public sealed class ToggleGemLockPacket : Packet {
        /// <summary>
        /// Gets or sets the gem lock's X coordinate.
        /// </summary>
        public short GemLockX { get; set; }

        /// <summary>
        /// Gets or sets the gem lock's Y coordinate.
        /// </summary>
        public short GemLockY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gem lock is locked.
        /// </summary>
        public bool IsGemLockLocked { get; set; }

        private protected override PacketType Type => PacketType.ToggleGemLock;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{IsGemLockLocked} @ ({GemLockX}, {GemLockY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            GemLockX = reader.ReadInt16();
            GemLockY = reader.ReadInt16();
            IsGemLockLocked = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(GemLockX);
            writer.Write(GemLockY);
            writer.Write(IsGemLockLocked);
        }
    }
}