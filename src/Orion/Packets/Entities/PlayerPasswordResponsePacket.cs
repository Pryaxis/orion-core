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
using JetBrains.Annotations;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent from the client to the server to try a password. This is sent in response to a
    /// <see cref="PlayerPasswordChallengePacket"/>.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerPasswordResponsePacket : Packet {
        [NotNull] private string _playerPassword = "";

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerPasswordResponse;

        /// <summary>
        /// Gets or sets the player's password.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        [NotNull]
        public string PlayerPassword {
            get => _playerPassword;
            set {
                _playerPassword = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{PlayerPassword}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerPassword = reader.ReadString();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerPassword);
        }
    }
}
