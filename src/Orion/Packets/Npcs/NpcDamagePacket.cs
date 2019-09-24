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
using JetBrains.Annotations;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent to damage an NPC.
    /// </summary>
    [PublicAPI]
    public sealed class NpcDamagePacket : Packet {
        private short _npcIndex;
        private short _damage;
        private float _knockback;
        private sbyte _hitDirection;
        private bool _isCriticalHit;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcDamage;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
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
        public float Knockback {
            get => _knockback;
            set {
                _knockback = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the hit direction. Values are -1 or 1.
        /// </summary>
        public sbyte HitDirection {
            get => _hitDirection;
            set {
                _hitDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsCriticalHit {
            get => _isCriticalHit;
            set {
                _isCriticalHit = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} for {Damage} hp, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _damage = reader.ReadInt16();
            _knockback = reader.ReadSingle();
            _hitDirection = (sbyte)(reader.ReadByte() - 1);
            _isCriticalHit = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_damage);
            writer.Write(_knockback);
            writer.Write((byte)(_hitDirection + 1));
            writer.Write(IsCriticalHit);
        }
    }
}
