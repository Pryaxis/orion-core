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

using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Packets.Projectiles {
    /// <summary>
    /// Packet sent to remove a projectile by identity. For the server, the identity is the absolute source of truth on
    /// projectile indices.
    /// </summary>
    public sealed class ProjectileRemoveIdentityPacket : Packet {
        private short _projectileIdentity;
        private byte _projectileOwnerPlayerIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ProjectileRemoveIdentity;

        /// <summary>
        /// Gets or sets the projectile identity.
        /// </summary>
        public short ProjectileIdentity {
            get => _projectileIdentity;
            set {
                _projectileIdentity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile owner's player index.
        /// </summary>
        public byte ProjectileOwnerPlayerIndex {
            get => _projectileOwnerPlayerIndex;
            set {
                _projectileOwnerPlayerIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ProjectileIdentity}), P={ProjectileOwnerPlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _projectileIdentity = reader.ReadInt16();
            _projectileOwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_projectileIdentity);
            writer.Write(_projectileOwnerPlayerIndex);
        }
    }
}
