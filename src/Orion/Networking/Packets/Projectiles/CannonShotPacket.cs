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
    /// Packet sent from the server to the client to shoot from a cannon.
    /// </summary>
    public sealed class CannonShotPacket : Packet {
        private short _shotDamage;
        private float _shotKnockback;
        private short _cannonTileX;
        private short _cannonTileY;
        private short _shotAngle;
        private short _shotAmmoType;
        private byte _shooterPlayerIndex;

        /// <inheritdoc />
        public override PacketType Type => PacketType.CannonShot;

        /// <summary>
        /// Gets or sets the shot's damage.
        /// </summary>
        public short ShotDamage {
            get => _shotDamage;
            set {
                _shotDamage = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the shot's knockback.
        /// </summary>
        public float ShotKnockback {
            get => _shotKnockback;
            set {
                _shotKnockback = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the cannon tile's X coordinate.
        /// </summary>
        public short CannonTileX {
            get => _cannonTileX;
            set {
                _cannonTileX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the cannon tile's Y coordinate.
        /// </summary>
        public short CannonTileY {
            get => _cannonTileY;
            set {
                _cannonTileY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the shot's angle.
        /// </summary>
        public short ShotAngle {
            get => _shotAngle;
            set {
                _shotAngle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the shot's ammo type.
        /// </summary>

        // TODO: implement an enum for this.
        public short ShotAmmoType {
            get => _shotAmmoType;
            set {
                _shotAmmoType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the shooter's player index.
        /// </summary>
        public byte ShooterPlayerIndex {
            get => _shooterPlayerIndex;
            set {
                _shooterPlayerIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ShooterPlayerIndex}, {ShotAmmoType} @ ({CannonTileX}, {CannonTileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ShotDamage = reader.ReadInt16();
            ShotKnockback = reader.ReadSingle();
            CannonTileX = reader.ReadInt16();
            CannonTileY = reader.ReadInt16();
            ShotAngle = reader.ReadInt16();
            ShotAmmoType = reader.ReadInt16();
            ShooterPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ShotDamage);
            writer.Write(ShotKnockback);
            writer.Write(CannonTileX);
            writer.Write(CannonTileY);
            writer.Write(ShotAngle);
            writer.Write(ShotAmmoType);
            writer.Write(ShooterPlayerIndex);
        }
    }
}
