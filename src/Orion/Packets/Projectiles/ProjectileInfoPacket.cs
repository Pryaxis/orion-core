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
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;
using Orion.Projectiles;
using Orion.Utils;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Packets.Projectiles {
    /// <summary>
    /// Packet sent to set projectile information.
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

        private readonly DirtiableArray<float> _projectileAiValues =
            new DirtiableArray<float>(TerrariaProjectile.maxAI);

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || _projectileAiValues.IsDirty;

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
        /// Gets or sets the projectile's position. The components are pixel-based.
        /// </summary>
        public Vector2 ProjectilePosition {
            get => _projectilePosition;
            set {
                _projectilePosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's velocity. The components are pixel-based.
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
        public IArray<float> ProjectileAiValues => _projectileAiValues;

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
            _projectileAiValues.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ProjectileIdentity}, {ProjectileType} @ ({ProjectilePosition}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _projectileIdentity = reader.ReadInt16();
            _projectilePosition = reader.ReadVector2();
            _projectileVelocity = reader.ReadVector2();
            _projectileKnockback = reader.ReadSingle();
            _projectileDamage = reader.ReadInt16();
            _projectileOwnerPlayerIndex = reader.ReadByte();
            _projectileType = (ProjectileType)reader.ReadInt16();

            Terraria.BitsByte header = reader.ReadByte();
            if (header[0]) _projectileAiValues._array[0] = reader.ReadSingle();
            if (header[1]) _projectileAiValues._array[1] = reader.ReadSingle();
            if (header[2]) _projectileUuid = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_projectileIdentity);
            writer.Write(_projectilePosition);
            writer.Write(_projectileVelocity);
            writer.Write(_projectileKnockback);
            writer.Write(_projectileDamage);
            writer.Write(_projectileOwnerPlayerIndex);
            writer.Write((short)_projectileType);

            Terraria.BitsByte header = 0;
            header[0] = _projectileAiValues._array[0] != 0;
            header[1] = _projectileAiValues._array[1] != 0;
            header[2] = _projectileUuid >= 0;

            writer.Write(header);
            if (header[0]) writer.Write(_projectileAiValues._array[0]);
            if (header[1]) writer.Write(_projectileAiValues._array[1]);
            if (header[2]) writer.Write(_projectileUuid);
        }
    }
}
