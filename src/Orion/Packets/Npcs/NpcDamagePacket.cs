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

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent to damage an NPC.
    /// </summary>
    /// <remarks>
    /// This packet is sent when a player damages an NPC or when some non-player source damages an NPC.
    /// </remarks>
    public sealed class NpcDamagePacket : Packet {
        private short _npcIndex;
        private short _damage;
        private float _knockback;
        private bool _hitDirection;
        private bool _isCriticalHit;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcDamage;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
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
        /// Gets or sets a value indicating the hit direction.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the hit is to the right; <see langword="false"/> if the hit is to the left.
        /// </value>
        public bool HitDirection {
            get => _hitDirection;
            set {
                _hitDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        /// <value><see langword="true"/> if the hit is critical; otherwise, <see langword="false"/>.</value>
        public bool IsCriticalHit {
            get => _isCriticalHit;
            set {
                _isCriticalHit = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _damage = reader.ReadInt16();
            _knockback = reader.ReadSingle();
            _hitDirection = reader.ReadByte() == 2;
            _isCriticalHit = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_damage);
            writer.Write(_knockback);
            writer.Write((byte)(_hitDirection ? 2 : 0));
            writer.Write(IsCriticalHit);
        }
    }
}
