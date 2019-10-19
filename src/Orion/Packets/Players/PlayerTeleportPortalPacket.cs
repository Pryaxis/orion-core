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
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent from the client to the server to teleport a player through a portal.
    /// </summary>
    public sealed class PlayerTeleportPortalPacket : Packet {
        private byte _playerIndex;
        private short _portalIndex;
        private Vector2 _playerNewPosition;
        private Vector2 _playerNewVelocity;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerTeleportPortal;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the portal index.
        /// </summary>
        public short PortalIndex {
            get => _portalIndex;
            set {
                _portalIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's new position. The components are pixel-based.
        /// </summary>
        public Vector2 PlayerNewPosition {
            get => _playerNewPosition;
            set {
                _playerNewPosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's new velocity. The components are pixel-based.
        /// </summary>
        public Vector2 PlayerNewVelocity {
            get => _playerNewVelocity;
            set {
                _playerNewVelocity = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} @ {PlayerNewPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _portalIndex = reader.ReadInt16();
            _playerNewPosition = reader.ReadVector2();
            _playerNewVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_portalIndex);
            writer.Write(in _playerNewPosition);
            writer.Write(in _playerNewVelocity);
        }
    }
}
