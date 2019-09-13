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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Networking.Packets.Extensions;
using Terraria.Localization;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the server to the client to disconnect it. This is sent as a response to an invalid
    /// <see cref="PasswordResponsePacket"/> or for various other reasons.
    /// </summary>
    public sealed class DisconnectPacket : Packet {
        private NetworkText _reason = NetworkText.Empty;

        /// <summary>
        /// Gets or sets the disconnection reason.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkText Reason {
            get => _reason;
            set => _reason = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal override PacketType Type => PacketType.Disconnect;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Reason}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _reason = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(Reason);
        }
    }
}
