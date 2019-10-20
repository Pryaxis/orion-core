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

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to shoot from a cannon.
    /// </summary>
    /// <remarks>This packet is sent whenever a cannon is triggered with wiring.</remarks>
    public sealed class CannonShotPacket : Packet {
        private short _damage;
        private float _knockback;
        private short _x;
        private short _y;
        private short _shotAngle;
        private short _shotType;
        private byte _shooterIndex;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.CannonShot;

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The damage.</value>
        public short Damage {
            get => _damage;
            set {
                _damage = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        /// <value>The knockback.</value>
        public float Knockback {
            get => _knockback;
            set {
                _knockback = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the cannon's X coordinate.
        /// </summary>
        /// <value>The cannon's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the cannon's Y coordinate.
        /// </summary>
        /// <value>The cannon's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        // TODO: explain this

        /// <summary>
        /// Gets or sets the shot's angle.
        /// </summary>
        /// <value>The shot's angle.</value>
        public short Angle {
            get => _shotAngle;
            set {
                _shotAngle = value;
                _isDirty = true;
            }
        }

        // TODO: implement an enum for this.

        /// <summary>
        /// Gets or sets the shot type.
        /// </summary>
        /// <value>The shot type.</value>
        public short ShotType {
            get => _shotType;
            set {
                _shotType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the shooter's player index.
        /// </summary>
        /// <value>The shooter's player index.</value>
        public byte ShooterIndex {
            get => _shooterIndex;
            set {
                _shooterIndex = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _damage = reader.ReadInt16();
            _knockback = reader.ReadSingle();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _shotAngle = reader.ReadInt16();
            _shotType = reader.ReadInt16();
            _shooterIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_damage);
            writer.Write(_knockback);
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_shotAngle);
            writer.Write(_shotType);
            writer.Write(_shooterIndex);
        }
    }
}
