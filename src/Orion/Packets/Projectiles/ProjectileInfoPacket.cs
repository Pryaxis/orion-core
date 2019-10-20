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
using Microsoft.Xna.Framework;
using Orion.Projectiles;
using Orion.Utils;
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Packets.Projectiles {
    /// <summary>
    /// Packet sent to set projectile information.
    /// </summary>
    /// <remarks>This packet is used to synchronize projectile state.</remarks>
    public sealed class ProjectileInfoPacket : Packet {
        private short _identity;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _knockback;
        private short _damage;
        private byte _ownerIndex;
        private ProjectileType _projectileType;
        private short _uuid = -1;
        private DirtiableArray<float> _aiValues = new DirtiableArray<float>(TerrariaProjectile.maxAI);

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _aiValues.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ProjectileInfo;

        /// <summary>
        /// Gets or sets the projectile's identity.
        /// </summary>
        /// <value>The projectile's identity.</value>
        /// <remarks>
        /// This property's value is tangentially related to the projectile index. It is initialized when a projectile
        /// spawns and is transmitted according to the sender's view of the projectiles array. As such, when searching
        /// for the projectile index, the owner index must be matched as well.
        /// </remarks>
        public short Identity {
            get => _identity;
            set {
                _identity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's position. The components are pixels.
        /// </summary>
        /// <value>The projectile's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's velocity. The components are pixels per tick.
        /// </summary>
        /// <value>The projectile's velocity.</value>
        public Vector2 Velocity {
            get => _velocity;
            set {
                _velocity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's knockback.
        /// </summary>
        /// <value>The projectile's knockback.</value>
        public float Knockback {
            get => _knockback;
            set {
                _knockback = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the projectile's damage.
        /// </summary>
        /// <value>The projectile's damage.</value>
        public short Damage {
            get => _damage;
            set {
                _damage = value;
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

        /// <summary>
        /// Gets or sets the projectile's type.
        /// </summary>
        /// <value>The projectile's type.</value>
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
        /// <value>The projectile's AI values.</value>
        public IArray<float> AiValues => _aiValues;

        /// <summary>
        /// Gets or sets the projectile's UUID.
        /// </summary>
        /// <value>The projectile's UUID.</value>
        public short Uuid {
            get => _uuid;
            set {
                _uuid = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _aiValues.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _identity = reader.ReadInt16();
            _position = reader.ReadVector2();
            _velocity = reader.ReadVector2();
            _knockback = reader.ReadSingle();
            _damage = reader.ReadInt16();
            _ownerIndex = reader.ReadByte();
            _projectileType = (ProjectileType)reader.ReadInt16();

            Terraria.BitsByte header = reader.ReadByte();
            var aiValues = new float[_aiValues.Count];
            if (header[0]) aiValues[0] = reader.ReadSingle();
            if (header[1]) aiValues[1] = reader.ReadSingle();

            _aiValues = new DirtiableArray<float>(aiValues);
            if (header[2]) _uuid = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_identity);
            writer.Write(in _position);
            writer.Write(in _velocity);
            writer.Write(_knockback);
            writer.Write(_damage);
            writer.Write(_ownerIndex);
            writer.Write((short)_projectileType);

            Terraria.BitsByte header = 0;
            header[0] = _aiValues[0] != 0;
            header[1] = _aiValues[1] != 0;
            header[2] = _uuid >= 0;

            writer.Write(header);
            if (header[0]) writer.Write(_aiValues[0]);
            if (header[1]) writer.Write(_aiValues[1]);
            if (header[2]) writer.Write(_uuid);
        }
    }
}
