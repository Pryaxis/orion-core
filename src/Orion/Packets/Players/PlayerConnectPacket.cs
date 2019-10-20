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

using System;
using System.IO;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the client to the server to start connecting.
    /// </summary>
    /// <remarks>This packet is always the first packet to be sent from the client when connecting.</remarks>
    public sealed class PlayerConnectPacket : Packet {
        private string _versionString = string.Empty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerConnect;

        /// <summary>
        /// Gets or sets the player's version string.
        /// </summary>
        /// <value>The player's version string.</value>
        /// <remarks>
        /// The version string restricts what client versions can connect to the server. It takes the form
        /// <c>Terraria###</c>, where <c>###</c> is the version number.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string VersionString {
            get => _versionString;
            set {
                _versionString = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _versionString = reader.ReadString();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_versionString);
    }
}
