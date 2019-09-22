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
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Networking.Packets.Extensions;
using Orion.Utils;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent to set a projectile's information.
    /// </summary>
    public sealed class ProjectileInfoPacket : Packet {
        private short _projectileIdentity;
        private Vector2 _projectilePosition;
        private Vector2 _projectileVelocity;
        private float _projectileKnockback;
        private short _projectileDamage;
        private byte _projectileOwnerPlayerIndex;
        private ProjectileType _projectileType;
        private short _projectileUuid = -1;

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || ProjectileAiValues.IsDirty;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ProjectileInfo;

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
        /// Gets or sets the projectile's position.
        /// </summary>
        public Vector2 ProjectilePosition {
            get => _projectilePosition;
            set {
                _projectilePosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's velocity.
        /// </summary>
        public Vector2 ProjectileVelocity {
            get => _projectileVelocity;
            set {
                _projectileVelocity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's knockback.
        /// </summary>
        public float ProjectileKnockback {
            get => _projectileKnockback;
            set {
                _projectileKnockback = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's damage.
        /// </summary>
        public short ProjectileDamage {
            get => _projectileDamage;
            set {
                _projectileDamage = value;
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

        /// <summary>
        /// Gets or sets the projectile's type.
        /// </summary>
        public ProjectileType ProjectileType {
            get => _projectileType;
            set {
                _projectileType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the projectile's AI values.
        /// </summary>
        public DirtiableArray<float> ProjectileAiValues { get; } = new DirtiableArray<float>(Terraria.Projectile.maxAI);

        /// <summary>
        /// Gets or sets the projectile's UUID.
        /// </summary>
        public short ProjectileUuid {
            get => _projectileUuid;
            set {
                _projectileUuid = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            ProjectileAiValues.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ProjectileIdentity}, {ProjectileType} @ ({ProjectilePosition}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ProjectileIdentity = reader.ReadInt16();
            ProjectilePosition = reader.ReadVector2();
            ProjectileVelocity = reader.ReadVector2();
            ProjectileKnockback = reader.ReadSingle();
            ProjectileDamage = reader.ReadInt16();
            ProjectileOwnerPlayerIndex = reader.ReadByte();
            ProjectileType = (ProjectileType)reader.ReadInt16();

            Terraria.BitsByte header = reader.ReadByte();
            if (header[0]) ProjectileAiValues[0] = reader.ReadSingle();
            if (header[1]) ProjectileAiValues[1] = reader.ReadSingle();
            if (header[2]) ProjectileUuid = reader.ReadInt16();
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ProjectileIdentity);
            writer.Write(ProjectilePosition);
            writer.Write(ProjectileVelocity);
            writer.Write(ProjectileKnockback);
            writer.Write(ProjectileDamage);
            writer.Write(ProjectileOwnerPlayerIndex);
            writer.Write((short)ProjectileType);

            Terraria.BitsByte header = 0;
            header[0] = ProjectileAiValues[0] != 0;
            header[1] = ProjectileAiValues[1] != 0;
            header[2] = ProjectileUuid >= 0;

            writer.Write(header);
            if (header[0]) writer.Write(ProjectileAiValues[0]);
            if (header[1]) writer.Write(ProjectileAiValues[1]);
            if (header[2]) writer.Write(ProjectileUuid);
        }
    }
}
