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

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Projectiles {
    /// <summary>
    /// Packet sent to remove a projectile.
    /// </summary>
    public sealed class RemoveProjectilePacket : Packet {
        /// <summary>
        /// Gets or sets the projectile identity.
        /// </summary>
        public short ProjectileIdentity { get; set; }

        /// <summary>
        /// Gets or sets the projectile owner's player index.
        /// </summary>
        public byte ProjectileOwnerPlayerIndex { get; set; }

        internal override PacketType Type => PacketType.RemoveProjectile;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ProjectileIdentity}), P={ProjectileOwnerPlayerIndex}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ProjectileIdentity = reader.ReadInt16();
            ProjectileOwnerPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ProjectileIdentity);
            writer.Write(ProjectileOwnerPlayerIndex);
        }
    }
}
