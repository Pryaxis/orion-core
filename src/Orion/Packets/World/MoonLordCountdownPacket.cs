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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Orion.Packets.Extensions;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to set the Moon Lord countdown.
    /// </summary>
    public sealed class MoonLordCountdownPacket : Packet {
        private TimeSpan _moonLordCountdown;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.MoonLordCountdown;

        /// <summary>
        /// Gets or sets the Moon Lord countdown.
        /// </summary>
        public TimeSpan MoonLordCountdown {
            get => _moonLordCountdown;
            set {
                _moonLordCountdown = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{MoonLordCountdown}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _moonLordCountdown = reader.ReadTimeSpan(4);

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_moonLordCountdown, 4);
    }
}
