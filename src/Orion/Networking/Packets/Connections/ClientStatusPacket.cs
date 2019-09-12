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
    /// Packet sent from the server to the client to set the client's status.
    /// </summary>
    public sealed class ClientStatusPacket : Packet {
        private NetworkText _statusText = NetworkText.Empty;

        /// <summary>
        /// Gets or sets the status increase.
        /// </summary>
        public int StatusIncrease { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkText StatusText {
            get => _statusText;
            set => _statusText = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.ClientStatus;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{StatusText}, I={StatusIncrease}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            StatusIncrease = reader.ReadInt32();
            _statusText = reader.ReadNetworkText();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(StatusIncrease);
            writer.Write(StatusText);
        }
    }
}
