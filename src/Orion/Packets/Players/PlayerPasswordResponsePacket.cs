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
    /// Packet sent from the client to the server to try a password.
    /// </summary>
    /// <remarks>This packet is sent in response to a <see cref="PlayerPasswordChallengePacket"/>.</remarks>
    public sealed class PlayerPasswordResponsePacket : Packet {
        private string _passwordAttempt = string.Empty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerPasswordResponse;

        /// <summary>
        /// Gets or sets the player's password attempt.
        /// </summary>
        /// <value>The player's password attempt.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string PasswordAttempt {
            get => _passwordAttempt;
            set {
                _passwordAttempt = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _passwordAttempt = reader.ReadString();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_passwordAttempt);
    }
}
