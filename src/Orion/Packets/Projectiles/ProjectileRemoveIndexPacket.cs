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
    /// Packet sent from the client to the server to remove a projectile by index.
    /// </summary>
    /// <remarks>This packet is sent to remove portals.</remarks>
    public sealed class ProjectileRemoveIndexPacket : Packet {
        private short _projectileIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ProjectileRemoveIndex;

        /// <summary>
        /// Gets or sets the projectile index.
        /// </summary>
        /// <value>The projectile index.</value>
        public short ProjectileIndex {
            get => _projectileIndex;
            set {
                _projectileIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) =>
            _projectileIndex = reader.ReadInt16();

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) =>
            writer.Write(_projectileIndex);
    }
}
