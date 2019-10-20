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

using System.IO;

namespace Orion.Packets.Projectiles {
    /// <summary>
    /// Packet sent to remove a projectile by identity.
    /// </summary>
    /// <remarks>This packet is sent to remove projectiles.</remarks>
    public sealed class ProjectileRemoveIdentityPacket : Packet {
        private short _identity;
        private byte _ownerIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ProjectileRemoveIdentity;

        /// <summary>
        /// Gets or sets the projectile's identity.
        /// </summary>
        /// <value>The projectile's identity.</value>
        /// <seealso cref="ProjectileInfoPacket.Identity"/>
        public short Identity {
            get => _identity;
            set {
                _identity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile owner's player index.
        /// </summary>
        /// <value>The projectile owner's player index.</value>
        public byte OwnerIndex {
            get => _ownerIndex;
            set {
                _ownerIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _identity = reader.ReadInt16();
            _ownerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_identity);
            writer.Write(_ownerIndex);
        }
    }
}
