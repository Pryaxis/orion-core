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

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to shoot from a cannon.
    /// </summary>
    public sealed class CannonShotPacket : Packet {
        private short _shotDamage;
        private float _shotKnockback;
        private short _cannonX;
        private short _cannonY;
        private short _shotAngle;
        private short _shotAmmoType;
        private byte _shooterPlayerIndex;

        /// <inheritdoc/>
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
        /// Gets or sets the cannon's X coordinate.
        /// </summary>
        public short CannonX {
            get => _cannonX;
            set {
                _cannonX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the cannon's Y coordinate.
        /// </summary>
        public short CannonY {
            get => _cannonY;
            set {
                _cannonY = value;
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

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[#={ShooterPlayerIndex}, {ShotAmmoType} @ ({CannonX}, {CannonY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _shotDamage = reader.ReadInt16();
            _shotKnockback = reader.ReadSingle();
            _cannonX = reader.ReadInt16();
            _cannonY = reader.ReadInt16();
            _shotAngle = reader.ReadInt16();
            _shotAmmoType = reader.ReadInt16();
            _shooterPlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_shotDamage);
            writer.Write(_shotKnockback);
            writer.Write(_cannonX);
            writer.Write(_cannonY);
            writer.Write(_shotAngle);
            writer.Write(_shotAmmoType);
            writer.Write(_shooterPlayerIndex);
        }
    }
}
